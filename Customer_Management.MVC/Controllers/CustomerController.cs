using Customer_Management.MVC.Contracts;
using Customer_Management.MVC.Models.Customer;
using Customer_Management.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Management.MVC.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            var customers = await _customerService.GetCustomers();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerDetails(id);
            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCustomerVM createCustomer)
        {
            try
            {
                var response = await _customerService.CreateCustomer(createCustomer);
                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(createCustomer);

        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerDetails(id);
            return View(customer);
        }

        // Put: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CustomerVM customer)
        {
            try
            {
                var response = await _customerService.UpdateCustomer(id, customer);
                if (response.Success)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(customer);

        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerDetails(id);
            return View(customer);
        }

        // Delete: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _customerService.DeleteCustomer(id);
                if (response.Success)
                {
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return BadRequest();
        }
    }
}
