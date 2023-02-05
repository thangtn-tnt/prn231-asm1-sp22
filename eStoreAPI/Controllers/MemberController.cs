using BusinessObject;
using DataAccess.Dto;
using DataAccess.DTO;
using DataAccess.Repositories;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IMemberRepository _repository = new MemberRepository();
        public MemberController()
        {
            _response = new();
        }

        [HttpGet]
        public ActionResult<APIResponse> GetMembers([FromQuery] string? search)
        {
            try
            {
                IEnumerable<MemberDTO> memList;

                memList = _repository.GetMembers();

                if (!string.IsNullOrEmpty(search))
                {
                    memList = memList.Where(u => u.Email.ToLower().Contains(search));
                }

                _response.Result = memList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
