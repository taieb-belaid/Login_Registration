#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace LoginRegistration.Models;

public class LogUser
{
    [Required]
    [EmailAddress]
    public string LogEmail{get;set;}
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)] 
    public string LogPassword{get;set;}
}   