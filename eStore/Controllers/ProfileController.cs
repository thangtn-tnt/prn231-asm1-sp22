using eStore.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using AutoMapper;
using eStore.Services;
using eStore.Models;

namespace eStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IMemberService _service;
        public ProfileController(IMemberService service, IMapper mapper)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionEmail)))
            {
                if (HttpContext.Session.GetInt32(SD.SessionID).HasValue)
                {
                    var response = await _service.GetAsync<APIResponse>((int)HttpContext.Session.GetInt32(SD.SessionID));
                    if (response != null && response.IsSuccess)
                    {
                        MemberEditDTO mem = JsonConvert.DeserializeObject<MemberEditDTO>(Convert.ToString(response.Result));

                        return View(mem);
                    }
                }
                else
                {
                    return View();
                }             
            }
            return Redirect("/");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MemberEditDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Information has updated";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
