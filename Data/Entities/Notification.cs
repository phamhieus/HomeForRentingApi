using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
  public class Notification
  {
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public string ToUser { get; set; }

    public string Message { get; set; }

    public DateTime SentDate { get; set; }

    public bool IsSeen { get; set; }
  }
}
