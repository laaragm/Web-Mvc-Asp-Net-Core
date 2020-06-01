using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Models.ViewModel;
using WebMvc.Services;
using WebMvc.Services.Exceptions;

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
        public async Task<IActionResult> Index()
        {
            var list = await SellerService.FindAllAsync();

            //View is going to generate an IActionResult containing the list passed as argument 
            return View(list);
        }

        //Opens the form so we can registrate a new seller.
        public async Task<IActionResult> Create()
        {
            var departments = await DepartmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        //POST request
        [HttpPost]
        //In order to prevent our application from receiving CSRF attacks
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //In order to enable client-side validation even though JavaScript is disabled in the user's browser
            if (!ModelState.IsValid)
            {
                var departments = await DepartmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await SellerService.InsertAsync(seller);

            return RedirectToAction(nameof(Index));
        }

        //When the View() specified below is returned, the framework is going to search for a screen called Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //id.Value is necessary because our id might be null
            var seller = await SellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await SellerService.RemoveAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException exception)
            {
                return RedirectToAction(nameof(Error), new { message = exception.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var seller = await SellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var seller = await SellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await DepartmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //In order to enable client-side validation even though JavaScript is disabled in the user's browser
            if (!ModelState.IsValid)
            {
                var departments = await DepartmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await SellerService.UpdateAsync(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException exception)
            {
                return RedirectToAction(nameof(Error), new { message = exception.Message });
            }
            catch (DbConcurrencyException exception)
            {
                return RedirectToAction(nameof(Error), new { message = exception.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                //Gets internal id request
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}