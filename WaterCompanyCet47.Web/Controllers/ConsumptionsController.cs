using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Data.Repositories;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Controllers
{
    public class ConsumptionsController : Controller
    {
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IUserHelper _userHelper;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly DataContext _dataContext;

        public ConsumptionsController(
            IConsumptionRepository consumptionRepository,
            IUserHelper userHelper,
            IEquipmentRepository equipmentRepository,
            DataContext dataContext)
        {
            _consumptionRepository = consumptionRepository;
            _userHelper = userHelper;
            _equipmentRepository = equipmentRepository;
            _dataContext = dataContext;
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
                Equipments = _equipmentRepository.GetComboEquipments(this.User.Identity.Name)


            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddConsumption(AddConsumptionViewModel model)
        {

            await _consumptionRepository.AddItemToConsumptionAsync(model, this.User.Identity.Name);
            return this.RedirectToAction("Create");


            //return this.View(model);
        }

        public async Task<IActionResult> ConfirmConsumption()
        {
            var response = await _consumptionRepository.ConfirmConsumptionAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }


    }
}
