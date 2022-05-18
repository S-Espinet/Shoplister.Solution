using System.ComponentModel.DataAnnotations;

namespace Shoplister.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "User Name: ")]
    public string UserName { get; set; }


    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password: ")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password: ")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
  }
}