using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UAECovidAPI.Authentication;
using UAECovidAPI.DataClass;
using UAECovidAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UAECovidAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UAECovidController : ControllerBase
    {
        private readonly ICountryData _country;

        public UAECovidController(ICountryData country)
        {
            _country = country;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCovidSummary")]
        public IActionResult GetCovidSummary(string slug)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.covid19api.com/");
                //HTTP GET
                var responseTask = client.GetAsync("summary");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Global>();
                    readTask.Wait();

                    var covidSummaries = readTask.Result;

                    Countries covidSummary = covidSummaries.Countries.Where(x => x.Slug.Equals(slug)).FirstOrDefault();
                    if (covidSummary == null)
                    {
                        return Ok( new Response { Status = "Success" , Message = "No Data Exists" , Data = null});
                        }
                    return Ok(new Response { Status = "Success", Message = "Successful", Data = covidSummary });

                }
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUAECovidSummary")]
        public IActionResult GetUAECovidDetails()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.covid19api.com/");
                //HTTP GET
                var responseTask = client.GetAsync("summary");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Global>();
                    readTask.Wait();

                    var covidSummaries = readTask.Result;

                    Countries covidSummary = covidSummaries.Countries.Where(x => x.Slug.Equals("united-arab-emirates")).FirstOrDefault();

                    if (covidSummary == null)
                    {
                        return Ok(new Response { Status = "Success", Message = "No Data Exists", Data = null });
                    }
                    return Ok(new Response { Status = "Success", Message = "Successful", Data = covidSummary });

                }
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCovidHistory")]
        public IActionResult GetCovidHistory(string slug)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.covid19api.com/");
                //HTTP GET
                var responseTask = client.GetAsync("total/country/"+slug);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AllCovidStatus>>();
                    readTask.Wait();

                    var covidHistory = readTask.Result;

                    return Ok(new Response { Status = "Success", Message = "Successful", Data = covidHistory });

                }
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUAECovidHistory")]
        public IActionResult GetUAECovidHistory()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.covid19api.com/");
                //HTTP GET
                var responseTask = client.GetAsync("total/country/united-arab-emirates");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AllCovidStatus>>();
                    readTask.Wait();

                    var covidHistory = readTask.Result;

                    return Ok(new Response { Status = "Success", Message = "Successful", Data = covidHistory });

                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("AddCountry")]
        public IActionResult AddCountryToDB(CountryClass country)
        {
            if (ModelState.IsValid)
            {
                int retValue = _country.AddCountry(country);
                if (retValue == 1)
                {
                    return Ok(new Response { Status = "Success", Message = "Successful Insertion. Country Id in Data field", Data = country.Id });
                }
                else
                {
                    return Ok(new Response { Status = "Error", Message = "Country already exists", Data = 0 });
                }

            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("UpdateCountry")]
        public IActionResult UpdateCountry(CountryClass country)
        {
            if (ModelState.IsValid)
            {
                int retValue = _country.UpdateCountry(country);
                if (retValue == 1)
                {
                    return Ok(new Response { Status="Success",Message="Update Success",Data = null });
                }
                else if (retValue == 0)
                {
                    return Ok(new Response { Status = "Error", Message = "Update Failed", Data = null });
                }
                else
                {
                    return Ok(retValue);
                }
                
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("DeleteCountry")]
        public IActionResult DeleteCountry(int countryCode)
        {
            if (ModelState.IsValid)
            {
                int retValue = _country.DeleteCountry(countryCode);
                if (retValue == 1)
                {
                    return Ok(new Response { Status = "Success", Message = "Delete Success", Data = null });
                }
                else if (retValue == 0)
                {
                    return Ok(new Response { Status = "Error", Message = "Delete Failed", Data = null });
                }
                else
                {
                    return Ok(retValue);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllCountries")]
        public IActionResult GetAllCountries()
        {

            List<CountryClass> allCountries = _country.GetAllCountries();
            return Ok(new Response { Status="Success",Message="Successful" , Data = allCountries } );

        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("GetCountry")]
        public IActionResult GetCountry(int Id)
        {
            CountryClass country = _country.GetCountry(Id);
            return Ok(new Response { Status = "Success", Message = "Successful", Data = country });

        }
    }
}
