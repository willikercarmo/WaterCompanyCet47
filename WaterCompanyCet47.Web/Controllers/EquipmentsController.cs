using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Data.Repositories;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Migrations;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserHelper _userHelper;


        public EquipmentsController(IEquipmentRepository equipmentRepository, IUserHelper userHelper)
        {
            _equipmentRepository = equipmentRepository;
            _userHelper = userHelper;
        }



        // GET: Equipments
        public IActionResult Index()
        {
            var equipments = _equipmentRepository.GetAll().OrderBy(e => e.WaterMetering);

            return View(equipments);

            //return View(_equipmentRepository.GetAll().OrderBy(e => e.WaterMetering));
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _equipmentRepository.GetByIdAsync(id.Value);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        [Authorize(Roles = "Admin")]
        // GET: Equipments/Create
        public IActionResult Create()
        {
            var model = new AddEquipmentViewModel
            {
                Users = _userHelper.GetComboUsers()            
            };

            return View(model);

        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,WaterMetering,Installation,Address")] Equipment equipment)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        //equipment.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //        equipment.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //        await _equipmentRepository.CreateAsync(equipment);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(equipment);
        //}


        [HttpPost]
        public async Task<IActionResult> Create(AddEquipmentViewModel model)
        {
            if (this.ModelState.IsValid)
            {

                await _equipmentRepository.AddEquipmentToEquipAsync(model, this.User.Identity.Name);


                return this.RedirectToAction("Create");
            }

            return this.View(model);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _equipmentRepository.GetByIdAsync(id.Value);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WaterMetering,Installation,Address")] Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    equipment.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _equipmentRepository.UpdateAsync(equipment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _equipmentRepository.ExistsAsync(equipment.Id))
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
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _equipmentRepository.GetByIdAsync(id.Value);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);
            await _equipmentRepository.DeleteAsync(equipment);

            return RedirectToAction(nameof(Index));
        }

        //private bool EquipmentExists(int id)
        //{
        //    //return this.equipmentRepository.ExistsAsync(view.Id);

        //    //return this.Equipments.Any(e => e.Id == id);
        //}
    }
}
