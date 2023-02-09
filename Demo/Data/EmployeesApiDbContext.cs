using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class EmployeesApiDbContext : DbContext
    {
        public EmployeesApiDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
