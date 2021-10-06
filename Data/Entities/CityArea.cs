using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
  public class CityArea
  {
    [Key]
    [MaxLength(5)]
    public string AreaCode { get; set; }

    [MaxLength(100)]
    public string AreaName { get; set; }

    [MaxLength(30)]
    public string AreaType { get; set; }
  }
}
