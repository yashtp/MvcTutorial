using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccessLayer;
using Helper;
using EmployeeContext = DataAccessLayer.EmployeeContext;

namespace MvcTut.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            EmployeeContext emp = new EmployeeContext();
            List<Department> dep = emp.Departments.ToList();
            return View(dep);
        }
    }
}