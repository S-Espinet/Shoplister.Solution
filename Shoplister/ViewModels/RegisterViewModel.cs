using System.ComponentModel.DataAnnotations;

namespace Shoplister.ViewModels
{
  public class RegisterViewModel
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required.")] 
    [DisplayFormat(ConvertEmptyStringToNull = false)]   
    [Display(Name = "User Name ")]
    public string UserName { get; set; }


    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]  
    [DataType(DataType.Password)]
    [Display(Name = "Password ")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password: ")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
  }
}