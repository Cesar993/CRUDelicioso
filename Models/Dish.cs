#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicioso.Models;
public class Dish
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Chef { get; set; }
    [Required]
     [Range(1, 5, ErrorMessage = "El sabor debe estar entre 1 y 5")]
    [MayorAUno]
    public int Tastines { get; set; }
    [Required]
    public string Description { get; set; }
    
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}
public class MayorAUno : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        int tastines = (int)value;
        if (tastines <= 0)
        {
            return new ValidationResult("El numero debe ser positivo");
        } else {
            return ValidationResult.Success;
        }
    }
}