using System;
using Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Responses
{
  public class RoomDetailResponse : RoomDto
  {
    public RoomImageDto ThumbnailImage { get; set; }

    public IEnumerable<RoomImageDto> DescriptionImages { get; set; }

    public UserDetailResponse Owner { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
  }
}
