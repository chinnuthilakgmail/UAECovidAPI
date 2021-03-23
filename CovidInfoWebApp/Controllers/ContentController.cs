using CovidInfoWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidInfoWebApp.Controllers
{
    public class ContentController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContentController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/GetAllCountries", RestSharp.Method.GET, null, null, null);
            if (response.Status == "Success")
            {

                return View(JsonConvert.DeserializeObject<IList<CountryClass>>(response.Data.ToString()));
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryClass model)
        {
            try
            {
                Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/AddCountry", RestSharp.Method.POST, model, null, _httpContextAccessor.HttpContext.Request.Cookies["Token"]);
                if (response.Status == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", id);
            Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/GetCountry", RestSharp.Method.GET, null, parameters, _httpContextAccessor.HttpContext.Request.Cookies["Token"]);
            if (response.Status == "Success")
            {

                return View(JsonConvert.DeserializeObject<CountryClass>(response.Data.ToString()));
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CountryClass model)
        {
            try
            {
                Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/UpdateCountry", RestSharp.Method.PUT, model, null, _httpContextAccessor.HttpContext.Request.Cookies["Token"]);
                if (response.Status == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", id);
            Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/GetCountry", RestSharp.Method.GET, null, parameters, _httpContextAccessor.HttpContext.Request.Cookies["Token"]);
            if (response.Status == "Success")
            {

                return View(JsonConvert.DeserializeObject<CountryClass>(response.Data.ToString()));
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("countryCode", id);
                Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/DeleteCountry", RestSharp.Method.DELETE, null, parameters, _httpContextAccessor.HttpContext.Request.Cookies["Token"]);
                if (response.Status == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult UAECovidSummary()
        {
            Response response = Utilities.UtilityClass.WebRequestWithToken<CovidSummary>("api/UAECovid/GetUAECovidSummary", RestSharp.Method.GET, null, null, null);
            if (response.Status == "Success")
            {
                return View(JsonConvert.DeserializeObject<CovidSummary>(response.Data.ToString()));
            }
            return View();
        }

        public ActionResult UAECovidHistory()
        {
            Response response = Utilities.UtilityClass.WebRequestWithToken<CovidHistory>("api/UAECovid/GetUAECovidHistory", RestSharp.Method.GET, null, null, null);
            if (response.Status == "Success")
            {
                return View(JsonConvert.DeserializeObject<IList<CovidHistory>>(response.Data.ToString()));
            }
            return View();
        }

         
        public ActionResult CovidSummary(string slug)
        {
            Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/GetAllCountries", RestSharp.Method.GET, null, null, null);
            if (response.Status == "Success")
            {
                IList<CountryClass> allCountries = JsonConvert.DeserializeObject<IList<CountryClass>>(response.Data.ToString());
                List<SelectListItem> selectListItems = allCountries.Select(x => new SelectListItem { Text = x.Country, Value = x.Slug }).ToList();
                ViewBag.AllCountries = selectListItems;
            }
            if (!string.IsNullOrEmpty(slug))
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("slug", slug);
                response = Utilities.UtilityClass.WebRequestWithToken<CovidSummary>("api/UAECovid/GetCovidSummary", RestSharp.Method.GET, null, parameters, null);
                if (response.Status == "Success" && response.Data != null)
                {
                    return View(JsonConvert.DeserializeObject<CovidSummary>(response.Data.ToString()));
                }
            }


            return View();
        }

        public ActionResult CovidHistory(string slug)
        {
            Response response = Utilities.UtilityClass.WebRequestWithToken<CountryClass>("api/UAECovid/GetAllCountries", RestSharp.Method.GET, null, null, null);
            if (response.Status == "Success")
            {
                IList<CountryClass> allCountries = JsonConvert.DeserializeObject<IList<CountryClass>>(response.Data.ToString());
                List<SelectListItem> selectListItems = allCountries.Select(x => new SelectListItem { Text = x.Country, Value = x.Slug }).ToList();
                ViewBag.AllCountries = selectListItems;
            }
            if (!string.IsNullOrEmpty(slug))
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("slug", slug);
                response = Utilities.UtilityClass.WebRequestWithToken<CovidHistory>("api/UAECovid/GetCovidHistory", RestSharp.Method.GET, null, parameters, null);
                if (response.Status == "Success" && response.Data != null)
                {
                    return View(JsonConvert.DeserializeObject<IList<CovidHistory>>(response.Data.ToString()));
                } 
            }
            return View();
        }
    }
}
