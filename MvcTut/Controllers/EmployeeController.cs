using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTut.Models;

namespace MvcTut.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Details(int id)
        {
            EmployeeContext emp = new EmployeeContext();
            Employee employee = emp.Employees.Single(x => x.EmployeeId == id);
            return View(employee);
        }

        public ActionResult Index()
        {
            EmployeeContext emp = new EmployeeContext();
            List<Employee> employee = emp.Employees.ToList();
            return View(employee);
        }
    }
}