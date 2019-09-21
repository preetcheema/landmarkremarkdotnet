using System;
using LandmarkRemark.Domain;
using LandmarkRemark.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandmarkRemark.Persistence
{
   public class LandmarkRemarkContext : DbContext
   {
      public LandmarkRemarkContext(DbContextOptions<LandmarkRemarkContext> options):base(options)
      {
         
      }
      public DbSet<User> Users { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(LandmarkRemarkContext).Assembly);
      }
   }
}