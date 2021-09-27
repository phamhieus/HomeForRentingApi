using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
  public class Message
  {
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid FromUser { get; set; }

    [ForeignKey(nameof(User))]
    public Guid ToUser { get; set; }

    public string Content { get; set; }

    public DateTime SentDate { get; set; }
  }
}
