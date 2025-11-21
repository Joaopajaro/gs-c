using Microsoft.EntityFrameworkCore;
using MentiaApi.Models;

namespace MentiaApi.Data
{
    /// <summary>
    /// Database context for the Mentia API. This class manages the connection to the underlying
    /// SQLite database and exposes DbSet properties for each entity in the domain. It also configures
    /// relationships between entities.
    /// </summary>
    public class MentiaDbContext : DbContext
    {
        public MentiaDbContext(DbContextOptions<MentiaDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Mentoria> Mentorias => Set<Mentoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between Role and User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Configure the one-to-many relationship between Mentor and Mentoria
            modelBuilder.Entity<Mentoria>()
                .HasOne(m => m.Mentor)
                .WithMany()
                .HasForeignKey(m => m.MentorId);
        }
    }
}