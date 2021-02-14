using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Data.Repositories;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Models;


namespace WaterCompanyCet47.Web.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IHostingEnvironment _hostingEnvironment;



        public InvoicesController(
            DataContext dataContext,
            IUserHelper userHelper,
            IInvoiceRepository invoiceRepository,
            IConsumptionRepository consumptionRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _invoiceRepository = invoiceRepository;
            _consumptionRepository = consumptionRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        public async Task<IActionResult> Index()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return View(_userHelper.GetAll().OrderBy(u => u.FullName));
            }

            var invoices = _userHelper.GetAll()
            .Where(u => u.UserName == user.UserName)
            .OrderBy(u => u.FullName);
            return View(invoices);
        }

        public async Task<IActionResult> DataHistory(string id)
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                var model = await _invoiceRepository.GetInvoiceAsync(id);
                return View(model);
            }
            else
            {
                var model = await _invoiceRepository.GetInvoiceAsync(user.Id);
                return View(model);
            }
            
        }

        public async Task<IActionResult> CalculateInvoice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (InvoiceExists(id.Value) == true)
            {
                return RedirectToAction("Index");
            }

            var model = await _dataContext.Consumptions
                .Include(u => u.User)
                .ThenInclude(e => e.Equipments)
                .Where(u => u.Id == id).FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            double valorTotal = 0;

            for (int i = 1; i <= model.ConsumptionValue; i++)
            {
                if (i <= 5)
                {
                    valorTotal += 0.30;
                }
                else if (i > 5 && i <= 15)
                {
                    valorTotal += 0.80;
                }
                else if (i > 15 && i <= 25)
                {
                    valorTotal += 1.20;
                }
                else
                {
                    valorTotal += 1.60;
                }
            }

            var invoice = new Invoice
            {
                Equipment = model.Equipment,
                Consumption = model,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = valorTotal,
                User = model.User
            };

            _dataContext.Invoices.Add(invoice);

            var consumptionUpdate = await _dataContext.Consumptions.FirstOrDefaultAsync(c => c.Id == model.Id);

            var contact = new Consumption { Id = model.Id };
            contact.InvoiceIssued = true;
            _dataContext.Entry(contact).Property("InvoiceIssued").IsModified = true;


            await _dataContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        private bool InvoiceExists(int id)
        {
            return _dataContext.Invoices.Any(e => e.Consumption.Id == id);
        }


        public async Task<IActionResult> Invoice(string id)
        {
            id = id.Replace(id.Substring(id.IndexOf(";")), "");

            var idDesenc = InvoiceSecurity.DecryptString(id);
                   
            var model = await _dataContext.Invoices
                .Include(u => u.User)
                .Include(e => e.Equipment)
                .Include(c => c.Consumption)
                .Where(u => u.Id == Convert.ToInt32(idDesenc))
                .FirstOrDefaultAsync();

            return View(model);
        }

    }
}
