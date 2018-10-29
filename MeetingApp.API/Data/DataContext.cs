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
    }
}