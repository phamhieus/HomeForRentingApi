using Data.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace AspImp.SwaggerExample
{
  public class ResetPasswordRequestExample : IExamplesProvider<ResetPasswordDto>
  {
    public ResetPasswordDto GetExamples()
    {
      return new ResetPasswordDto
      {
        RecentPassword = "Password100;",
        NewPassword = "Password1001;",
        ConfirmPassword = "Password1001;",
      };
    }
  }
}
