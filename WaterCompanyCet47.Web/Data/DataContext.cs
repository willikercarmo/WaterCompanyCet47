using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Data
{
    public class DataContext : DbContext // DbContext é uma propriedade da Entity Framework
    {

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

    }
}
