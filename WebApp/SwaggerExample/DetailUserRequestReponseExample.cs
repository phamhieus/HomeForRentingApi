using Data.DTO.Responses;

using Swashbuckle.AspNetCore.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspImp.SwaggerExample
{
  public class DetailUserRequestReponseExample : IExamplesProvider<UserDetailResponse>
  {
    public UserDetailResponse GetExamples()
    {
      return new UserDetailResponse
      {
        UserName = "phamhieu",
        Email = "phamhieutb.dev@gmail.com",
        PhoneNumber = "022222",
        ThumbnailImage = new Data.DTO.UserImageDto
        {
          UserId = "7960e79f-94c5-4dcc-9ab5-7531b94b23aa",
          ImageId = Guid.Parse("63bd645a-d3bb-4a4e-1558-08d986d939bd"),
          Url = "http:103.159.50.133:5000/images/room/phamhieu-63bd645a-d3bb-4a4e-1558-08d986d939bd.png",
          ImageName = "phamhieu-63bd645a-d3bb-4a4e-1558-08d986d939bd.png"
        }
      };
    }
  }
}
