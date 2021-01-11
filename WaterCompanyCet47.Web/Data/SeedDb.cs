using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Helpers;

namespace WaterCompanyCet47.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
      

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync(); // EnsureCreatedAsync vai verificar se a base de dados existe

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            var user = await this.userHelper.GetUserByEmailAsync("williker.do.carmo@formandos.cinel.pt");
            var user2 = await this.userHelper.GetUserByEmailAsync("williker.do.carmo@formandos.cinel.pt");
            var user3 = await this.userHelper.GetUserByEmailAsync("williker.do.carmo@formandos.cinel.pt");
            var user4 = await this.userHelper.GetUserByEmailAsync("williker.do.carmo@formandos.cinel.pt");
            var user5 = await this.userHelper.GetUserByEmailAsync("williker.do.carmo@formandos.cinel.pt");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Williker",
                    LastName = "Carmo",
                    Email = "williker.do.carmo@formandos.cinel.pt",
                    UserName = "williker.do.carmo@formandos.cinel.pt",
                    PhoneNumber = "912277715"
                };

                user2 = new User
                {
                    FirstName = "Pedro",
                    LastName = "Águas",
                    Email = "pedro@water.com",
                    UserName = "pedro@water.com",
                    PhoneNumber = "912277700"
                };

                user3 = new User
                {
                    FirstName = "Ana",
                    LastName = "Filipa",
                    Email = "ana@water.com",
                    UserName = "ana@water.com",
                    PhoneNumber = "912277701"
                };

                user4 = new User
                {
                    FirstName = "Antonio",
                    LastName = "Reis",
                    Email = "antonio@water.com",
                    UserName = "antonio@water.com",
                    PhoneNumber = "912277702"
                };

                user5 = new User
                {
                    FirstName = "Filipa",
                    LastName = "Neves",
                    Email = "filipa@water.com",
                    UserName = "filipa@water.com",
                    PhoneNumber = "912277703"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

                if (!this.context.Equipments.Any()) // se estiver vazia
            {
                this.AddEquipment("CT10", "Morada 1", user2);
                this.AddEquipment("CT20", "Morada 2", user3);
                this.AddEquipment("CT30", "Morada 3", user4);
                this.AddEquipment("CT40", "Morada 4", user5);
                await this.context.SaveChangesAsync(); //vamos gravar

            }

        }

        private void AddEquipment(string name, string address, User user)
        {
            this.context.Equipments.Add(new Equipment
            {
                WaterMetering = name,
                Address = address,
                User = user
            });
        }
    }
}
