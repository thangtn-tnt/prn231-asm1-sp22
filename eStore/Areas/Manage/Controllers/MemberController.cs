using AutoMapper;
using BusinessObject;
using eStore.Models;
using eStore.Models.Dto;
using eStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStore.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class MemberController : Controller
    {
        private readonly IMemberService _service;
        private readonly IMapper _mapper;
        public MemberController(IMemberService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? search)
        {
            List<MemberDTO> listMembers = new List<MemberDTO>();

            var response = await _service.GetAllAsync<APIResponse>(search);

            if (response != null && response.IsSuccess)
            {
                listMembers = JsonConvert.DeserializeObject<List<MemberDTO>>(Convert.ToString(response.Result));
            }

            return View(listMembers);
        }
        [HttpGet]
        public async Task<ActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(RegisterationRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreateAsync<APIResponse>(requestDTO);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Add");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                MemberDTO model = JsonConvert.DeserializeObject<MemberDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MemberDTO model)
        {
            var response = await _service.DeleteAsync<APIResponse>(model.MemberId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _service.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                MemberEditDTO mem = JsonConvert.DeserializeObject<MemberEditDTO>(Convert.ToString(response.Result));

                return View(mem);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberEditDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Edit));
        }


    }
}
