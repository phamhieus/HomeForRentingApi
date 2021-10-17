using System;
using Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Responses
{
  public class RoomSamuryResponse : RoomDto
  {
    public RoomImageDto ThumbnailImage { get; set; }

    public double Distance { get; set; }
  }
}
