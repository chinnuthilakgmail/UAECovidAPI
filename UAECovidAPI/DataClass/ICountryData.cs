using System.Collections.Generic;
using UAECovidAPI.Models;

namespace UAECovidAPI.DataClass
{
    public interface ICountryData
    {
        int AddCountry(CountryClass model);
        int DeleteCountry(int code);
        int UpdateCountry(CountryClass model);
        List<CountryClass> GetAllCountries();
        CountryClass GetCountry(int id);
    }
}