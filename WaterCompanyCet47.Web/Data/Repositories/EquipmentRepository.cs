namespace WaterCompanyCet47.Web.Data.Repositories
{
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WaterCompanyCet47.Web.Helpers;

    public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public EquipmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
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
