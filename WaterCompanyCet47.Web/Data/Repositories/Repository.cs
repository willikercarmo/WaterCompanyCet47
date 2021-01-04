namespace WaterCompanyCet47.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WaterCompanyCet47.Web.Data.Entities;

    public class Repository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Equipment> GetEquipments()
        {
            return this.context.Equipments.OrderBy(e => e.WaterMetering);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.context.Users.OrderBy(e => e.FullName);
        }

        public Equipment GetEquipment(int id)
        {
            return this.context.Equipments.Find(id);
        }

        public void AddEquipment(Equipment equipment)
        {
            this.context.Equipments.Add(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            this.context.Equipments.Update(equipment);
        }

        public void RemoveEquipment(Equipment equipment)
        {
            this.context.Equipments.Remove(equipment);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool EquipmentExists(int id)
        {
            return this.context.Equipments.Any(e => e.Id == id);
        }

    }
}
