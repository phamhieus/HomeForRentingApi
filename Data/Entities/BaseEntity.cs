using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
  public class BaseEntity
  {
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [ForeignKey(nameof(User))]
    public string CreatedBy { get; set; }

    [ForeignKey(nameof(User))]
    public string UpdatedBy { get; set; }
  }
}
