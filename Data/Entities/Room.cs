﻿using Data.Common;

using System;
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
    public string Description { get; set; }

    [Required(ErrorMessage = "Address is a required field.")]
    [MaxLength(500, ErrorMessage = "Maximum length for the Address is 500 characters.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Street is a required field.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Province is a required field.")]
    public string Province { get; set; }

    [Required(ErrorMessage = "City is a required field.")]
    public string City { get; set; }

    [Required(ErrorMessage = "Cost is a required field.")]
    public long Cost { get; set; }

    public GenderType Gender { get; set; }

    public int Mounth { get; set; }

    public int Year { get; set; }

    public RoomType Type { get; set; }

    public RentingType RoomType { get; set; }

    public RoomStatus Status { get; set; }
  }
}
