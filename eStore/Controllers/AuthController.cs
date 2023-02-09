using BusinessObject;
using eStore.Models;
using eStore.Models.Dto;
using eStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace eStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionEmail)))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            if (ModelState.IsValid)
            {
                var res = await _authService.LoginAsync<APIResponse>(obj);

                if (res != null && res.IsSuccess)
                {
                    LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(res.Result));

                    HttpContext.Session.SetString(SD.SessionEmail, model.Email);
                    HttpContext.Session.SetString(SD.SessionRole, model.Role);
                    if (model.Member.MemberId > 0)
                    {
                        HttpContext.Session.SetInt32(SD.SessionID, model.Member.MemberId);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", res.ErrorMessages.SingleOrDefault());
                }
            }
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionEmail)))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO obj)
        {
            if (ModelState.IsValid)
            {
                APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);
                if (result != null && result.IsSuccess)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("CustomError", result.ErrorMessages.SingleOrDefault());
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SD.SessionEmail);
            HttpContext.Session.Remove(SD.SessionID);
            HttpContext.Session.Remove(SD.SessionRole);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
