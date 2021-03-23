using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAECovidAPI.Models;

namespace UAECovidAPI.Data
{
    public class CovidDBContext : DbContext
    {
        public CovidDBContext(DbContextOptions<CovidDBContext> options) : base(options)
        {

        }
        public DbSet<CountryClass> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryClass>()
                .ToTable("Country")
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }

    }

    public enum ConnectionStrings
    {
        AspNetDatabase, CovidAPIDatabase
    }
}
