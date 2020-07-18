using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi.EmployeeService.Models;
using WebApi.Entities.Models;

namespace WebApi.EmployeeService.Controllers
{
    public class HomeController : Controller
    {
        EmployeeContext context = new EmployeeContext();

        public ActionResult Index()
        {
            var employees = context.Employees.ToList();
            return View(new EmployeeListModel() {
                Employees=employees
            });
        }

        public ActionResult GetEmployees()
        {
            return View();
        }
    }
}
