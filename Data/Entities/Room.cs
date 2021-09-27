﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
  public class Room : BaseEntity
  {
    [Required(ErrorMessage = "ShortName is a required field.")]
    [MaxLength(200, ErrorMessage = "Maximum length for the ShortName is 200 characters.")]
    public string ShortName { get; set; }

    [Required(ErrorMessage = "Discription is a required field.")]
    public string Discription { get; set; }

    [Required(ErrorMessage = "Address is a required field.")]
    [MaxLength(500, ErrorMessage = "Maximum length for the Address is 500 characters.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Service is a required field.")]
    public string Service { get; set; }

    [Required(ErrorMessage = "Cost is a required field.")]
    public long Cost { get; set; }

    public int Type { get; set; }
  }
}
