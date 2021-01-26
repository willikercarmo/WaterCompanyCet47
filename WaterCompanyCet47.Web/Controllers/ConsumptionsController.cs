using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Repositories;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Controllers
{
    public class ConsumptionsController : Controller
    {
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IUserHelper _userHelper;

        public ConsumptionsController(
            IConsumptionRepository consumptionRepository,
            IUserHelper userHelper)
        {
            _consumptionRepository = consumptionRepository;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _consumptionRepository.GetConsumptionAsync(this.User.Identity.Name);
           
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _consumptionRepository.GetConsumptionDetailTempsAsync(this.User.Identity.Name);

            return View(model);
        }

        public IActionResult AddConsumption()
        {
            var model = new AddConsumptionViewModel
            {
                Users = _userHelper.GetComboUsers()
            };

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddConsumption()
        //{
        //    var model = new AddConsumptionViewModel
        //    {
        //        Users = _userHelper.GetComboUsers()
        //    };

        //    return View(model);
        //}
    }
}
