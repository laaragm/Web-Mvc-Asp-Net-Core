using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Models.ViewModel;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService SellerService;
        private readonly DepartmentService DepartmentService;

        //DI
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            SellerService = sellerService;
            DepartmentService = departmentService;
        }

        //When the controller receives a call, then we need to return an action (in this case, View() called
        //Index). To summarize, the controller forwarded a requisition to View().
        public IActionResult Index()
        {
            var list = SellerService.FindAll();

            //View is going to generate an IActionResult containing the list passed as argument 
            return View(list);
        }

        //Opens the form so we can registrate a new seller.
        public IActionResult Create()
        {
            var departments = DepartmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        //POST request
        [HttpPost]
        //In order to prevent our application from receiving CSRF attacks
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            SellerService.Insert(seller);

            return RedirectToAction(nameof(Index));
        }
    }
}