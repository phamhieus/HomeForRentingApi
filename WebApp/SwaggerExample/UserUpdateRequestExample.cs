using Data.DTO;

using Swashbuckle.AspNetCore.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspImp.SwaggerExample
{
  public class UserUpdateRequestExample : IExamplesProvider<UserUpdateDto>
  {
    public UserUpdateDto GetExamples()
    {
      return new UserUpdateDto
      {
        UserName = "Dev99",
        PhoneNumber = "123123123",
      };
    }
  }
}
