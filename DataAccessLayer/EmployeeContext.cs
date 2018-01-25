using System.Data.Entity;
using Helper;

namespace DataAccessLayer
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentTotals> DepartmentTotals { get; set; }
    }
}