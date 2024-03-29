﻿
using Data.Configuration;
using Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  public class DBContext : IdentityDbContext<User> 
  {
    public DBContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new CompanyConfiguration());
      modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
      modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<CommentedUser> CommentedUsers { get; set; }
    public DbSet<RoomImage> RoomImages { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<ProvinceArea> ProvinceArea { get; set; }
    public DbSet<CityArea> CityAreas { get; set; }
    public DbSet<AwardArea> AwardAreas { get; set; }
  }
}
