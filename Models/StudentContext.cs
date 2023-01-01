using Microsoft.EntityFrameworkCore;

namespace TrackingDataChangesInEntityFramework.Controllers;

public class StudentContext : DbContext
{
    public StudentContext(DbContextOptions<StudentContext> options) : base(options)
    {
        
    }

    public DbSet<Student> Students { get; set; }
    
    //Enable Temporal Table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable(nameof(Students), studentsTable =>
        {
            studentsTable.IsTemporal();
        });
    }
}
