using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
  public class CommentedUserDto
  {
    public Guid Id { get; set; }

    public int RatingStar { get; set; }

    public string Content { get; set; }

    public string EvaluatedUser { get; set; }
    public string CommentByUserName { get; set; }

    public Guid ReferenceComment { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
  }
}
