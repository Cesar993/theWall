#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CuentasBancarias.Models;
public class User
{
    [Key]
    [Required]
    public int UserId { get; set; }

    [MinLength(3, ErrorMessage = "Oops el minimo es de 3 caracteres")]
    [Required]
    public string FirstName { get; set; }
    [MinLength(3, ErrorMessage = "Oops el minimo es de 3 caracteres")]
    [Required]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "por favor proporciona un correo valido")]
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public DateTime Created_at {get;set;} = DateTime.Now;
    public DateTime Updated_at {get;set;} = DateTime.Now;

    [NotMapped]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    [Display(Name = "Pasword confirmado")]
    public string PasswordConfirm { get; set; }


    public List<Transaction> ListaTransactions {get;set;}= new List<Transaction>();


}