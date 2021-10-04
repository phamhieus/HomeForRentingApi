using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
  public class UserImageDto
  {
    public string UserId { get; set; }

    public Guid ImageId { get; set; }

    public string Url { get; set; }

    public string ImageName { get; set; }
  }
}
