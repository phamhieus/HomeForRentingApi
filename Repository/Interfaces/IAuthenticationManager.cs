
using Data.DTO;

using System.Threading.Tasks;

namespace Repository.Interfaces
{
  public interface IAuthenticationManager
  {
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
  }
}
