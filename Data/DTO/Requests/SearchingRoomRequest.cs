using Data.Common;

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO.Requests
{
  public class SearchingRoomRequest
  {
    public string AwardId { get; set; }
    public string ProvinceId { get; set; }
    public string CityId { get; set; }
    public long? MinCost { get; set; }
    public long? MaxCost { get; set; }
    public RoomType? Type { get; set; }
    public RentingType? RoomType { get; set; }
    public GenderType? Gender { get; set; }

    public double? Longitude { set; get; }
    public double? Latitude { get; set; }
  }
}
