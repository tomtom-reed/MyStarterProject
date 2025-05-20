using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSource.Models;
using Microsoft.EntityFrameworkCore;

namespace DataSource;
public class ApplicationDbContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>().HasKey(u => u.Id);
        modelBuilder.Entity<Users>().HasMany(u => u.Activities);

        modelBuilder.Entity<UserActivity>().HasKey(ua => ua.Id);
    }

    
}