using Data.Common;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
  }
}
