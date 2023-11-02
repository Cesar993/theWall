#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CuentasBancarias.Models;
public class Transaction
{
    [Key]
    [Required]
    public int TransactionId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTime Created_at {get;set;} = DateTime.Now;
    public DateTime Updated_at {get;set;} = DateTime.Now;
    //LLAVE FORANEA
    public int UserId { get; set; }
    public User? Creador { get; set; }

}