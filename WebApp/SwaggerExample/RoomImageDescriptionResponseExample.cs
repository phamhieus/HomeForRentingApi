using Data.DTO;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace WebApp.SwaggerExample
{
  public class RoomImageDescriptionResponseExample : IExamplesProvider<IEnumerable<RoomImageDto>>
  {
    public IEnumerable<RoomImageDto> GetExamples()
    {
      return new RoomImageDto[]
      {
        new RoomImageDto()
        {
            ImageId = System.Guid.Parse("8823a833-c612-472d-6709-08d986065146"),
            RoomId = System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
            Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png",
            ImageName = "Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png"
        },
        new RoomImageDto() {
             ImageId = System.Guid.Parse("186d3294-84b7-42bd-ed3d-08d98605e7b3"),
             RoomId =   System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
             Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-186d3294-84b7-42bd-ed3d-08d98605e7b3.png",
             ImageName = "Phong-201-Tan-triu-186d3294-84b7-42bd-ed3d-08d98605e7b3.png"
        }
      };
    }
  }
}
