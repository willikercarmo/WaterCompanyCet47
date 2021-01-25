using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Repositories;

namespace WaterCompanyCet47.Web.Controllers
{
    public class ConsumptionsController : Controller
    {
        private readonly IConsumptionRepository _consumptionRepository;

        public ConsumptionsController(
            IConsumptionRepository consumptionRepository)
        {
            _consumptionRepository = consumptionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _consumptionRepository.GetConsumptionAsync(this.User.Identity.Name);
           
            return View(model);
        }
    }
}
