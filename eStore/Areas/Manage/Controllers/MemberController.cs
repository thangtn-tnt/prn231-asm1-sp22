using AutoMapper;
using BusinessObject;
using eStore.Models;
using eStore.Models.Dto;
using eStore.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Index()
        {
            List<MemberDTO> listMembers = new List<MemberDTO>();

            var response = await _service.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                listMembers = JsonConvert.DeserializeObject<List<MemberDTO>>(Convert.ToString(response.Result));
            }

            return View(listMembers);
        }
    }
}
