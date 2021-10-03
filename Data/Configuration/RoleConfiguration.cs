﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configuration
{
  public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
  {
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
      builder.HasData(
        new IdentityRole
        {
          Name = "Manager",
          NormalizedName = "MANAGER"
        },
        new IdentityRole
        {
          Name = "Administrator",
          NormalizedName = "ADMINISTRATOR"
        },
        new IdentityRole
        {
          Name = "User",
          NormalizedName = "USER"
        }
      );
    }
  }
}