namespace WaterCompanyCet47.Web.Data.Repositories
{
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WaterCompanyCet47.Web.Models;

    public interface IEquipmentRepository : IGenericRepository<Equipment>
    {


        IEnumerable<SelectListItem> GetComboEquipments();

        Task AddEquipmentToEquipAsync(AddEquipmentViewModel model, string username);

    }

}
