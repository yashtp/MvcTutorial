using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTut.Models;

namespace MvcTut
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //
            Database.SetInitializer<EmployeeContext>(null);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
