using Demo.DAL.Contexts.configurations;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAppDbContext : DbContext
    {

        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options):base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)                 //old Way With no depe ndensy injection
        //=> optionsBuilder.UseSqlServer("Server=.; Database=MVCAppDb; Trusted_Connection = True;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());  
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //All Conficurations in  DAL  
                }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
