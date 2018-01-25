using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;
using Helper;
using DataAccessLayer;

using EmployeeContext = DataAccessLayer.EmployeeContext;

namespace MvcTut.Controllers
{
    public class EmployeeController : Controller
    {
        private EmpBusinessLayer _bal;

        public EmployeeController()
        {
            _bal = new EmpBusinessLayer();
        }
        // GET: Employee
        //public ActionResult Index(int? departmentId)
        //{
        //    var employee = new List<Employee>();
        //    var emp1 = new List<Employee>();
        //    EmployeeContext emp = new EmployeeContext();
        //    if (departmentId == 0 || departmentId == null)
        //    {
        //        employee = emp.Employees.ToList();
        //        return View(employee);
        //    }
        //    emp1 = emp.Employees.Where(e => e.DepartmentId == departmentId).ToList();
        //    return View(emp1);
        //}

        public ActionResult Details(int id)
        {
            EmployeeContext emp = new EmployeeContext();
            Employee employee = emp.Employees.Single(x => x.EmployeeId == id);
            return View(employee);
        }

        public ActionResult Index(int? DepartmentId)
        {
            List<Employee> emp = new List<Employee>();
            if (DepartmentId == null)
            {
                emp = _bal.GetEmployees();
            }
            else
            {
                emp = _bal.GetEmployees(DepartmentId);
            }
            return View(emp);
        }

        public ActionResult EmpByDepartment()
        {
            var emp = _bal.DepartmentTotals();
            return View(emp);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult Create(FormCollection formCollection)
        //{
        //    // you can also map it using manual parameter string name,string gender etc;
        //    // irrespective of parameter order
        //    // also can do Create(Employee employee) it will detect auto by name
        //    //employee.Name = name;
        //    //employee.Gender = gender;
        //    //employee.City = city;
        //    BusinessLayer.Employee employee = new BusinessLayer.Employee();
        //    employee.Name = formCollection["Name"];
        //    employee.Gender = formCollection["Gender"];
        //    employee.City = formCollection["City"];

        //    EmpBusinessLayer empb = new EmpBusinessLayer();
        //    var a = empb.AddEmployee(employee);
        //    if (a)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        Response.Write("faliure");
        //        return View();
        //    }
        //}

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Employee employee = new Employee();
            //employee model will be updated with key values
            TryUpdateModel(employee);
            if (ModelState.IsValid)
            {
                var a = _bal.AddEmployee(employee);
                if (a)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            var emp = _bal.GetSingleEmployee(id);
            return View(emp);
        }


        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            Employee employee = new Employee();
            employee = _bal.GetSingleEmployee(id);

            //Both include and exclude properties
            TryUpdateModel(employee, null, new[] { "EmployeeId", "Gender", "City" }, new[] { "Name" });

            //Only exclude, Name is excluded from updating thru fiddler
            //TryUpdateModel(employee, null, null, new string[] {"Name"});
            if (ModelState.IsValid)
            {
                var emp = _bal.UpdateEmployee(employee);
                if (emp)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }


        #region EditpostInterfaces
        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult Edit_Post(int id)
        //{
        //    Employee employee = new Employee();
        //    employee = _bal.GetSingleEmployee(id);


        //    //All properties of employee will be updated unless the interface item
        //    TryUpdateModel<IEmployee>(employee);

        //    //Only exclude, Name is excluded from updating thru fiddler
        //    //TryUpdateModel(employee, null, null, new string[] {"Name"});
        //    if (ModelState.IsValid)
        //    {
        //        var emp = _bal.UpdateEmployee(employee);
        //        if (emp)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}
        #endregion



        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult Edit_Post([Bind(Exclude = "Name",Include = "EmployeeId,Gender,City")]Employee employee)
        //{
        //    employee.Name = _bal.GetSingleEmployee(employee.EmployeeId).Name;

        //    //binding checks if it binds all value or not then passes it to model state
        //    //since name is excluded,validation fails and we need to remove [Required] from property
        //   if (ModelState.IsValid)
        //    {
        //        var emp = _bal.UpdateEmployee(employee);
        //        if (emp)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var res = _bal.DeleteEmployee(id);

            return RedirectToAction("Index");
        }

    }
}