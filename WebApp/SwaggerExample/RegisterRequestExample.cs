using Data.DTO;

using Swashbuckle.AspNetCore.Filters;

namespace AspImp.SwaggerExample
{
  public class RegisterRequestExample : IExamplesProvider<UserForRegistrationDto>
  {
    public UserForRegistrationDto GetExamples()
    {
      return new UserForRegistrationDto
      {
        UserName = "JDoe",
        Email = "phamhieutb.dev@gmail.com",
        Password = "Password100;",
        PhoneNumber = "33333",
      };
    }
  }
}
