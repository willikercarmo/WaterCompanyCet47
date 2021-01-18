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
        

        public EquipmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            

        }

        
    } 
}
