using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataAccessLayer;
using Helper;

namespace BusinessLayer
{
    public class EmpBusinessLayer
    {
        private EmpDataAccess _dal;
        private EmployeeContext _employeeContext;

        public EmpBusinessLayer()
        {
            _dal = new EmpDataAccess();
            _employeeContext = new EmployeeContext();
        }

        public List<Employee> GetEmployees()
        {
            var list = _dal.GetEmployees();

            return list;
        }

        public List<Employee> GetEmployees(int? id)
        {
            var list = _dal.GetEmployees(id);

            return list;
        }

        public Employee GetSingleEmployee(int id)
        {
            var emp = _employeeContext.Employees.Single(x => x.EmployeeId == id);

            return emp;
        }

        public bool AddEmployee(Employee employee)
        {
            var res = _dal.AddEmployee(employee);

            return res;
        }

        public bool UpdateEmployee(Employee employee)
        {
            var res = _dal.UpdateEmployee(employee);

            return res;
        }

        public bool DeleteEmployee(int id)
        {
            var res = _dal.DeleteEmployee(id);
            return res;
        }

        public ObservableCollection<DepartmentTotals> DepartmentTotals()
        {
            var list = _dal.GetDepartmentTotals();

            return list;
        }
    }
}
