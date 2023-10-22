using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<User> Users { get; set; }
       

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Note>().HasData(
        //        new Note()
        //        {
        //            Id = 1,
        //            NoteText = "Gracias por visitar esta aplicación",
        //            ColorId = 1,
        //            UserId = 2,
        //            Priority = "Alta",
        //            Date = DateTime.Now,
        //        });
        //}
    }
}
