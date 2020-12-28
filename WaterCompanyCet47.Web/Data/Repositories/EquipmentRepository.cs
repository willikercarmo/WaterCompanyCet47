namespace WaterCompanyCet47.Web.Data.Repositories
{
    using Entities;
    public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DataContext context) : base(context)
        {


        }

    }
}
