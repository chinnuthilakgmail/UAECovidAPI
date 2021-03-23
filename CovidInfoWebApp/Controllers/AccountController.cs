using CovidInfoWebApp.Models;
using CovidInfoWebApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidInfoWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {

            TokenClass token = Utilities.UtilityClass.GetToken(model);
            SetCookie("Token", token.Token, token.Expiration);

            return RedirectToAction("Index", "Content");
                //View();
        }
        [HttpPost]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("Token");
            return RedirectToAction("Index");
        }
        
        public IActionResult Register()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]        
        public IActionResult Register(RegisterModel model)
        {
            Response response = Utilities.UtilityClass.WebRequestWithToken<RegisterModel>("api/Authenticate/register-admin", RestSharp.Method.POST, model, null, _httpContextAccessor.HttpContext.Request.Cookies["Token"]);
            ViewBag.Message =response !=null ? response.Message : "Could not register user";
            return View();
        }
        public void SetCookie(string key, string value, DateTime expireTime)
        {
            CookieOptions option = new CookieOptions();


            option.Expires = expireTime;
            option.HttpOnly = true;
            option.Secure = true;

            Response.Cookies.Append(key, value, option);
        }
    }
}
