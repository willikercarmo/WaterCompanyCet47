namespace WaterCompanyCet47.Web.Data.Repositories
{
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WaterCompanyCet47.Web.Helpers;
    using WaterCompanyCet47.Web.Models;

    public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public EquipmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task AddEquipmentToEquipAsync(AddEquipmentViewModel model, string username)
        {
            //var equipment = await _context.Equipments.FindAsync(model.WaterMetering);
           
            var userlogado = await _userHelper.GetUserByEmailAsync(username);
            if (userlogado == null)
            {
                return;
            }

            if (await _userHelper.IsUserInRoleAsync(userlogado, "Admin"))
            {
                var user = await _context.Users.FindAsync(model.UserId);

                var equipment = new Equipment
                {
                    WaterMetering = model.WaterMetering,
                    Installation = model.Installation,
                    Address = model.Address,
                    User = user
                };

                _context.Equipments.Add(equipment);


                await _context.SaveChangesAsync();
            }

            return;
               
        }

        public IEnumerable<SelectListItem> GetComboEquipments()
        {
            var list = _context.Equipments.Select(p => new SelectListItem
            {
                Text = p.WaterMetering,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecione um contador)",
                Value = "0"
            });

            return list;
        }

        public Task<IQueryable<Equipment>> GetOrderAsync(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
