using Data.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace AspImp.SwaggerExample
{
  public class RoomImageThumbnailResponseExample : IExamplesProvider<RoomImageDto>
  {
    public RoomImageDto GetExamples()
    {
      return new RoomImageDto()
      {
        ImageId = Guid.Parse("8823a833-c612-472d-6709-08d986065146"),
        RoomId = Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
        Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png",
        ImageName = "Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png"
      };
    }
  }
}
