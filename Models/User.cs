#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginRegistration.Models;

public class User
{
    [Key]
    public int UserId{get;set;}
    [Required]
    [MinLength(3)]
    public string FirstName{get;set;}
    
    [Required]
    [MinLength(3)]
    public string LastName{get;set;}
    
    [Required]
    [EmailAddress]
    public string Email{get;set;}
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password{get;set;}
    [NotMapped]
    [Compare("Password")]
    public string ConfirmPassword{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.Now;
    public DateTime UpdatedAt{get;set;} = DateTime.Now;
}