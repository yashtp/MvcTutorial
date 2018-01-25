using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    [Table("tbldepartment")]
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    [Table("tbldepartment")]
    public class DepartmentTotals
    {
        public string Name { get; set; }

        public int Total { get; set; }
    }
}
