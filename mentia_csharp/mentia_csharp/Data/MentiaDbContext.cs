using Microsoft.EntityFrameworkCore;
using MentiaApi.Models;

namespace MentiaApi.Data
{

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

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Mentoria>()
                .HasOne(m => m.Mentor)
                .WithMany()
                .HasForeignKey(m => m.MentorId);
        }
    }
}
