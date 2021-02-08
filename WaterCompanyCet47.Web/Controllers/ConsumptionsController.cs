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
            return this.RedirectToAction("Index");

        }

        // GET: Consumptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumption = await _consumptionRepository.GetByIdAsync(id.Value);
            if (consumption == null)
            {
                return NotFound();
            }

            var model = new ModifyConsumptionViewModel
            {
                ConsumptionValue = consumption.ConsumptionValue,
                ForBegin = consumption.ForBegin,
                ForEnd = consumption.ForEnd,
                User = consumption.User,
                Equipments = _equipmentRepository.GetComboEquipments(this.User.Identity.Name)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConsumptionDate,Equipment,ForBegin,ForEnd,ConsumptionValue")] Consumption consumption)
        {
            if (id != consumption.Id)
            {
                return NotFound();
            }

            try
            {
                consumption.Equipment = await _equipmentRepository.GetByIdAsync(id);
                consumption.User = await _userHelper.GetByIdAsync(id.ToString());
                consumption.ConsumptionDate = DateTime.UtcNow;

                await _consumptionRepository.UpdateAsync(consumption);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _consumptionRepository.ExistsAsync(consumption.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }


    }
}
