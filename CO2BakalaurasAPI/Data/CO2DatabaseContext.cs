using CO2BakalaurasAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CO2BakalaurasAPI.Data
{
    public class CO2DatabaseContext : DbContext
    {
        public CO2DatabaseContext(DbContextOptions<CO2DatabaseContext> options):base(options)
        {
            
        }
        public DbSet<Administratorius> ADMINISTRATORIUS => Set<Administratorius>();
        public DbSet<Atlieka> ATLIEKA => Set<Atlieka>();
        public DbSet<Automobilis> AUTOMOBILIS => Set<Automobilis>();
        public DbSet<Butas> BUTAS => Set<Butas>();
        public DbSet<CO2> CO2 => Set<CO2>();
        public DbSet<Draugauja> DRAUGAUJA => Set<Draugauja>();
        public DbSet<Sanaudos> SANAUDOS => Set<Sanaudos>();
        public DbSet<Statistika> STATISTIKA => Set<Statistika>();
        public DbSet<Uzduotis> UZDUOTIS => Set<Uzduotis>();
        public DbSet<Vartotojas> VARTOTOJAS => Set<Vartotojas>();


    }
}
