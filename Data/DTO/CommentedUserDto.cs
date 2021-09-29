using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
  public class CommentedUserDto
  {
    public int RatingStar { get; set; }

    public string Content { get; set; }

    public Guid EvaluatedUser { get; set; }

    public CommentedUser ReferenceComment { get; set; }

    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
  }
}
