using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO.Responses
{
  public class UserDetailResponse
  {
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public UserImageDto ThumbnailImage { get; set; }
  }
}
