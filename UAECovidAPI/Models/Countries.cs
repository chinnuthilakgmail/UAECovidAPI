using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAECovidAPI.Models
{
    public class Countries
    {
        /*
          "Country": "ALA Aland Islands",
      "CountryCode": "AX",
      "Slug": "ala-aland-islands",
      "NewConfirmed": 0,
      "TotalConfirmed": 0,
      "NewDeaths": 0,
      "TotalDeaths": 0,
      "NewRecovered": 0,
      "TotalRecovered": 0,
      "Date": "2020-04-05T06:37:00Z"
         */
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public Int64 NewConfirmed { get; set; }
        public Int64 TotalConfirmed { get; set; }
        public Int64 NewDeaths { get; set; }
        public Int64 TotalDeaths { get; set; }
        public Int64 NewRecovered { get; set; }
        public Int64 TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }

    public class CountryClass
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
    }
}
