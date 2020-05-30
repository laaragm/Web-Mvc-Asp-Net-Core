using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departmentsList = new List<Department>();
            departmentsList.Add(new Department{ Id = 1, Name = "Technology" });
            departmentsList.Add(new Department { Id = 2, Name = "RH" });

            //Since we want to send controller 's data list (departmentsList) to View, we need to pass it as a 
            //parameter of View (which is responsible for showing the arguments received)
            return View(departmentsList);
        }
    }
}