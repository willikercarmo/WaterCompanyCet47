using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;

        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;

            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync(); // EnsureCreatedAsync vai verificar se a base de dados existe

            if (!this.context.Equipments.Any()) // se estiver vazia
            {
                this.AddEquipment("CT10", "Morada 1");
                this.AddEquipment("CT20", "Morada 2");
                this.AddEquipment("CT30", "Morada 3");
                this.AddEquipment("CT40", "Morada 4");
                await this.context.SaveChangesAsync(); //vamos gravar

            }

        }

        private void AddEquipment(string name, string address)
        {
            this.context.Equipments.Add(new Equipment
            {
                WaterMetering = name,
                Address = address
            });
        }
    }
}
