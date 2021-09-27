using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
  public class CommentedUser : BaseEntity
  {
    public int RatingStar { get; set; }

    public string Content { get; set; }

    [ForeignKey(nameof(User))]
    public Guid EvaluatedUser { get; set; }

    [ForeignKey(nameof(CommentedUser))]
    public Guid ReferenceComment { get; set; }
  }
}
