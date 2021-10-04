using System.ComponentModel.DataAnnotations;

namespace Data.DTO
{
  public class UserUpdateDto
  {
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; }
  }
}
