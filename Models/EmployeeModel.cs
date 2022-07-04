using System.ComponentModel.DataAnnotations;

namespace app01.Models;
public class EmployeeModel
{
    [Display(Name = "No")]
    public int Id {get;set;}

    [Required(ErrorMessage = "Phai co ten")]
    [Display(Name ="Name")]
    public string Name {get;set;}

    [Required(ErrorMessage ="Thieu email")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email {get;set;}
    
    [Required(ErrorMessage ="Thieu dia chi")]
    [Display(Name ="Address")]
    public string Address {get;set;}

    [Required]
    [StringLength(20, MinimumLength =10, ErrorMessage = "Kiem tra so dien thoai")]
    public string Phone {get;set;}
}