using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAECovidAPI.Models
{

    public class AllCovidStatus
    {
        /*
           
         {"Country":"United Arab Emirates","CountryCode":"","Province":"","City":"","CityCode":"","Lat":"0","Lon":"0","Confirmed":37642,"Deaths":274,"Recovered":20337,"Active":17031,"Date":"2020-06-05T00:00:00Z"}
         
         */
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public Int64 Confirmed { get; set; }
        public Int64 Deaths { get; set; }
        public Int64 Recovered { get; set; }
        public Int64 Active { get; set; }
        public DateTime Date { get; set; }
    }

   
   

   
}
