using EngineeringCentreDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringCentreDashboard.Data
{
    public class EngineeringDashboardDbContext : DbContext
    {
        public DbSet<ToDo> ToDoItems { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        public EngineeringDashboardDbContext(DbContextOptions<EngineeringDashboardDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>()
                .HasMany(t => t.ToDos)
                .WithOne(t => t.UserLogin)
                .HasForeignKey(t => t.UserLoginId);
                //.OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
