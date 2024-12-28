using System.Data.Entity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyProject.Domain;
using MyProject.Domain.DbQuery;

namespace MyProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region DbQuery
        public DbSet<EmployeeQuery> EmployeeQuery { get; set; }
        #endregion

        #region DbSets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager) 
                .WithOne() // No reverse navigation from Employee
                .HasForeignKey<Department>(d => d.ManagerId) // FK in Department
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading delete

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department) // Employee.Department navigation
                .WithMany() // No reverse navigation for all Employees
                .HasForeignKey(e => e.DepartmentId) // FK in Employee
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading delete

            modelBuilder.Entity<EmployeeQuery>().HasNoKey().ToView(null);


            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<PerformanceReview>().ToTable("PerformanceReviews");
        }
    }
}
