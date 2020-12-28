using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WaterCompanyCet47.Web.Data;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Data.Repositories;

namespace WaterCompanyCet47.Web.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly IEquipmentRepository equipmentRepository;

        public EquipmentsController(IEquipmentRepository equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        // GET: Equipments
        public IActionResult Index()
        {
            return View(this.equipmentRepository.GetAll().OrderBy(e => e.WaterMetering));
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await this.equipmentRepository.GetByIdAsync(id.Value);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WaterMetering,Installation,Address")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                await this.equipmentRepository.CreateAsync(equipment);                
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await this.equipmentRepository.GetByIdAsync(id.Value);
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
                    await this.equipmentRepository.UpdateAsync(equipment);              
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.equipmentRepository.ExistsAsync(equipment.Id))
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

            var equipment = await this.equipmentRepository.GetByIdAsync(id.Value);
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
            var equipment = await this.equipmentRepository.GetByIdAsync(id);
            await this.equipmentRepository.DeleteAsync(equipment);
        
            return RedirectToAction(nameof(Index));
        }

        //private bool EquipmentExists(int id)
        //{
        //    //return this.equipmentRepository.ExistsAsync(view.Id);
            
        //    //return this.Equipments.Any(e => e.Id == id);
        //}
    }
}
