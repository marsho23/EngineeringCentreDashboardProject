using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringCentreDashboard.Data
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDo> ToDoItems { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .ToTable("ToDoItems")
                .HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
