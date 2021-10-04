using Data.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace AspImp.SwaggerExample
{
  public class LoginRequestExample : IExamplesProvider<UserForAuthenticationDto>
  {
    public UserForAuthenticationDto GetExamples()
    {
      return new UserForAuthenticationDto
      {
        UserName = "phamhieutb.dev@gmail.com",
        Password = "Password100;",
      };
    }
  }
}
