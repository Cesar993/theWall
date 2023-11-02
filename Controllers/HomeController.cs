// Using statements
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using CuentasBancarias.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        // Now any time we want to access our database we use _context   

        List<User> ListaUsers = _context.Users.Include(tran => tran.ListaTransactions).ToList();

        return View("Index");
    }




    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return View("Index");
    }


    [SessionCheck]
    [HttpGet]
    [Route("account")]
    public IActionResult Account()
    {

        return View("Account");
    }


    //POST



    [HttpPost]

    [Route("user/Registrar")]
    public IActionResult UserRegistrar(User user)
    {
        // si todos los datos fueron validados
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Account");
        };
        return View("Index");
    }





 [HttpPost]
    [Route("procesa/login")]
    public IActionResult ProcesaLogin(Login login)
    {
        if (ModelState.IsValid)
        {
            User? usuario =_context.Users.FirstOrDefault(u=> u.Email == login.Email);
            
            if (usuario != null)
            {
            //DENCRIPTAMOS EL PASWORD PARA COMPARAR CREDENCIALES
            PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
            var result = Hasher.VerifyHashedPassword(login, usuario.Password, login.Password);
            if (result != 0)
            {
                HttpContext.Session.SetString("Nombre", usuario.FirstName);
                    HttpContext.Session.SetString("Apellido", usuario.LastName);
                    HttpContext.Session.SetString("Email", usuario.Email);
                    HttpContext.Session.SetInt32("Id", usuario.UserId);
                    return RedirectToAction("Account", "Home");
            }
            }
            ModelState.AddModelError("Password", "the usernem or pasword are incorrect");
            return View("Index");
           
        }
        return View("Index");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    // Name this anything you want with the word "Attribute" at the end




// Name this anything you want with the word "Attribute" at the end
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        string? email = context.HttpContext.Session.GetString("Email");

        // Check to see if we got back null
        if(email == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}



}
