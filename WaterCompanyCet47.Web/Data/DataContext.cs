using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<ConsumptionDetail> ConsumptionDetails { get; set; }

        public DbSet<ConsumptionDetailTemp> ConsumptionDetailTemps { get; set; }




        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //TODO: Modelo de Price na tablea. Video Parte 29 - Web login e logout (1/2)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

                            

            base.OnModelCreating(modelBuilder);
        }


    }
}
