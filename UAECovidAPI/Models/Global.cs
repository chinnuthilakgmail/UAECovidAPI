using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAECovidAPI.Models
{
    public class Global
    {
        /*
         "NewConfirmed": 100282,
    "TotalConfirmed": 1162857,
    "NewDeaths": 5658,
    "TotalDeaths": 63263,
    "NewRecovered": 15405,
    "TotalRecovered": 230845
         */
        public Int64 NewConfirmed { get; set; }
        public Int64 TotalConfirmed { get; set; }
        public Int64 NewDeaths { get; set; }
        public Int64 TotalDeaths { get; set; }
        public Int64 NewRecovered { get; set; }
        public Int64 TotalRecovered { get; set; }
        public List<Countries> Countries { get; set; }
        public DateTime Date { get; set; }
    }
}
