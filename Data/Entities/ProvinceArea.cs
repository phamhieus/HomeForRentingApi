using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
  public class ProvinceArea
  {
    [Key]
    [MaxLength(5)]
    public string AreaCode { get; set; }

    [MaxLength(100)]
    public string AreaName { get; set; }

    [MaxLength(30)]
    public string AreaType { get; set; }

    [MaxLength(5)]
    public string ParentArea { get; set; }
  }
}
