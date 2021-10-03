using Data.Common;
using Data.Entities;
using System;

namespace Data.DTO
{
  public class RoomImageDto
  {
    public Guid ImageId { get; set; }
   
    public Guid RoomId { get; set; }

    public string Url { get; set; }

    public string ImageName { get; set; }
  }
}
