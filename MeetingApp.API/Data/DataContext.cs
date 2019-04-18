using MeetingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Value> Values{get;set;}
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
            .HasKey(k => new {k.LikerId, k.LikeeId});

            builder.Entity<Like>()
            .HasOne(u => u.Likee)
            .WithMany(u=> u.Likers)
            .HasForeignKey(u=> u.LikeeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
            .HasOne(u => u.Liker)
            .WithMany(u=> u.Likees)
            .HasForeignKey(u=> u.LikerId)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}