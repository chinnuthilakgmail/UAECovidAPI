using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAECovidAPI.Authentication;
using UAECovidAPI.Models;

namespace UAECovidAPI.Data
{
    public class DbInitializer
    {

         
        public static void InitializeCovidDB(CovidDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Countries.Any())
            {
                return;
            }

            var Countries = new CountryClass[]
                {
                new CountryClass{ Code="HU",Country="Hungary",Slug="hungary"},
                new CountryClass{ Code="AU",Country="Australia",Slug="australia"}
                };

            foreach (CountryClass item in Countries)
            {
                context.Countries.Add(item);
            }
            context.SaveChanges();

        }
    }
}
