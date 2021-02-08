using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Data.Repositories;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SelectPdf;

namespace WaterCompanyCet47.Web.Controllers
{
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

        public IActionResult Index()
        {
            var user = _userHelper.GetAll().OrderBy(u => u.FullName);
            return View(user);
        }

        public async Task<IActionResult> DataHistory()
        {

            var model = await _invoiceRepository.GetInvoiceAsync(this.User.Identity.Name);         

            return View(model);
        }

        public async Task<IActionResult> CalculateInvoice(int? id)
        {
            if (id == null)
            {
                return NotFound();
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

            await _dataContext.SaveChangesAsync();

           
            return RedirectToAction(nameof(Index));
           
        }

        public IActionResult GeneratePdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
            byte[] pdf = pdfDocument.Save();
            pdfDocument.Close();

            return File(pdf, "application/pdf", "Fatura.pdf");
        }
    }
}
