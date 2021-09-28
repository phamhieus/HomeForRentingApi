using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DTO
{
  public class ResetPasswordDto
  {
    [Required(ErrorMessage = "Password is required")]
    public string RecentPassword { get; set; }

    [Required(ErrorMessage = "New Password is required")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm Password is required and macthed with new password"), Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
  }
}
