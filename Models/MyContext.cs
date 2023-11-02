#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CuentasBancarias.Models;
public class MyContext : DbContext 
{   
    //un dbset por cada tabla
    public DbSet<User> Users { get; set; } 
    // This line will always be here. It is what constructs our context upon initialization  
    public DbSet<Transaction> Transactions { get; set; }
    public MyContext(DbContextOptions options) : base(options) { }    

}
