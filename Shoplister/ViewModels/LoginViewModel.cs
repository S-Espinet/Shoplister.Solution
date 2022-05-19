using System.ComponentModel.DataAnnotations;

namespace Shoplister.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "User name is required.", AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]   
    [Display(Name = "User Name ")]
    public string UserName { get; set; }


    [Required(ErrorMessage = "Password is required.",AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]  
    [DataType(DataType.Password)]
    [Display(Name = "Password ")]
    public string Password { get; set; }
  }
}