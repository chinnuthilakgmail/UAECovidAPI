using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UAECovidAPI.Authentication;
using UAECovidAPI.Data; 
using UAECovidAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UAECovidAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UAECovidController : ControllerBase
    { 
        private readonly CovidDBContext context;

        public UAECovidController( CovidDBContext context)
        {
             
            this.context = context;
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
        public async Task<IActionResult> AddCountryToDB(CountryClass country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Countries.Add(new CountryClass { Country = country.Country,Slug = country.Slug,Code = country.Code});
                    await context.SaveChangesAsync();
                    return Ok(new Response { Status = "Success", Message = "Successful Insertion", Data = country.Id });
                }
                catch (DbUpdateException ex)
                {
                    return Ok(new Response { Status = "Error", Message = ex.Message, Data = null });
                } 
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("UpdateCountry")]
        public async Task<IActionResult> UpdateCountry(CountryClass country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Countries.Update(country);
                    await context.SaveChangesAsync();
                    return Ok(new Response { Status = "Success", Message = "Update Success", Data = null });
                }
                catch (DbUpdateException ex)
                {
                    return Ok(new Response { Status = "Error", Message = ex.Message, Data = null });
                }
                 

            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry(int countryCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CountryClass country = context.Countries.Where(x => x.Id == countryCode).FirstOrDefault();
                    context.Countries.Remove(country);
                    await context.SaveChangesAsync();
                    return Ok(new Response { Status = "Success", Message = "Delete Success", Data = null });
                }
                catch (DbUpdateException ex)
                {
                    return Ok(new Response { Status = "Error", Message = ex.Message, Data = null });
                }
                 
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllCountries")]
        public IActionResult GetAllCountries()
        { 
            List<CountryClass> allCountries = context.Countries.ToList();
            return Ok(new Response { Status="Success",Message="Successful" , Data = allCountries } );

        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("GetCountry")]
        public IActionResult GetCountry(int Id)
        { 
            CountryClass country = context.Countries.Where(x => x.Id == Id).FirstOrDefault();
            return Ok(new Response { Status = "Success", Message = "Successful", Data = country });

        }
    }
}
