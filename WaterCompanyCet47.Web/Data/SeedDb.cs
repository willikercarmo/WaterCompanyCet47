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
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync(); // EnsureCreatedAsync vai verificar se a base de dados existe

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            var user = await this.userHelper.GetUserByEmailAsync("admin@water.com");
            var user1 = await this.userHelper.GetUserByEmailAsync("admin@water.com");
            var user2 = await this.userHelper.GetUserByEmailAsync("admin@water.com");
            var user3 = await this.userHelper.GetUserByEmailAsync("admin@water.com");
            var user4 = await this.userHelper.GetUserByEmailAsync("admin@water.com");


            if (user == null)
            {
                user = new User
                {
                    FirstName = "Administrador",
                    LastName = "WaterCompany",
                    Email = "admin@water.com",
                    UserName = "admin@water.com",
                    PhoneNumber = "912277715",
                    Nif = _random.Next(100000000, 999999999).ToString()
                };
               
                user1 = new User
                {
                    FirstName = "Pedro",
                    LastName = "do Carmo",
                    Email = "pedro@water.com",
                    UserName = "pedro@water.com",
                    PhoneNumber = "9" + _random.Next(10000000, 99999999).ToString(),
                    Nif = _random.Next(100000000, 999999999).ToString()
                };
                

                user2 = new User
                {
                    FirstName = "Ana",
                    LastName = "do Carmo",
                    Email = "ana@water.com",
                    UserName = "ana@water.com",
                    PhoneNumber = "9" + _random.Next(10000000, 99999999).ToString(),
                    Nif = _random.Next(100000000, 999999999).ToString()
                };
               

                user3 = new User
                {
                    FirstName = "Antonio",
                    LastName = "do Carmo",
                    Email = "antonio@water.com",
                    UserName = "antonio@water.com",
                    PhoneNumber = "9" + _random.Next(10000000, 99999999).ToString(),
                    Nif = _random.Next(100000000, 999999999).ToString()
                };
                

                user4 = new User
                {
                    FirstName = "Filipa",
                    LastName = "do Carmo",
                    Email = "filipa@water.com",
                    UserName = "filipa@water.com",
                    PhoneNumber = "9" + _random.Next(10000000, 99999999).ToString(),
                    Nif = _random.Next(100000000, 999999999).ToString()
                };
                

                var result = await this.userHelper.AddUserAsync(user, "123456");
                var result1 = await this.userHelper.AddUserAsync(user1, "123456");
                var result2 = await this.userHelper.AddUserAsync(user2, "123456");
                var result3 = await this.userHelper.AddUserAsync(user3, "123456");
                var result4 = await this.userHelper.AddUserAsync(user4, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
                await this.userHelper.AddUserToRoleAsync(user1, "Customer");
                await this.userHelper.AddUserToRoleAsync(user2, "Customer");
                await this.userHelper.AddUserToRoleAsync(user3, "Customer");
                await this.userHelper.AddUserToRoleAsync(user4, "Customer");
            }

            var isRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!this.context.Equipments.Any()) // se estiver vazia
            {
                this.AddEquipment("Morada 1", user1);
                this.AddEquipment("Morada 2", user2);
                this.AddEquipment("Morada 3", user3);
                this.AddEquipment("Morada 4", user4);
                await this.context.SaveChangesAsync(); //vamos gravar

            }

        }

        private void AddEquipment(string address, User user)
        {
            this.context.Equipments.Add(new Equipment
            {
                WaterMetering = "CT" + _random.Next(100000000, 999999999).ToString(),
                Address = address,
                User = user
            });
        }
    }
}