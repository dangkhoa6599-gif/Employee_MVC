using Microsoft.EntityFrameworkCore;

namespace app01.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
     protected override void OnModelCreating (ModelBuilder builder) {

            base.OnModelCreating (builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes ()) {
                var tableName = entityType.GetTableName ();
                if (tableName.StartsWith ("AspNet")) {
                    entityType.SetTableName (tableName.Substring (6));
                }
            }
            // Auto-increment primary key
            builder.Entity<EmployeeModel>()
                    .Property(e =>e.Id)
                    .ValueGeneratedOnAdd();
     }
     public DbSet<app01.Models.EmployeeModel> EmployeeModels{get;set;}
}