using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovidInfoWebApp.Models
{
     
    public class CountryClass
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
    }
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class CovidSummary
    { 
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

    public class CovidHistory
    { 
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

    public class RegisterModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
