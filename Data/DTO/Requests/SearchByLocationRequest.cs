using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO.Requests
{
  public class SearchByLocationRequest
  {
    public int Longitude { set; get; }
    public int Latitude { get; set; }
  }
}
