using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers
{
    public class SellersController : Controller
    {
        //When the controller receives a call, then we need to return an action (in this case, View() called
        //Index). To summarize, the controller forwarded a requisition to View().
        public IActionResult Index()
        {
            return View();
        }
    }
}