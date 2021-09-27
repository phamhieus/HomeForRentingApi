using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DTO
{
  public class MessageDto
  {
    public Guid Id { get; set; }

    public Guid FromUser { get; set; }

    public Guid ToUser { get; set; }

    public string Content { get; set; }

    public DateTime SentDate { get; set; }
  }
}
