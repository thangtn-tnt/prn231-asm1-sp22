using BusinessObject;
using DataAccess.Dto;
using DataAccess.DTO;
using DataAccess.Repositories;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _repository.Login(model);
            if (loginResponse.Member == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest();
            }
            return Ok(loginResponse);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool isUnique = _repository.IsUniqueMember(model.Email);
            if (!isUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Email already exists");
                return BadRequest(_response);
            }

            var user = await _repository.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
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
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        public ActionResult<APIResponse> GetMember([FromRoute] int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var mem = _repository.GetMemberById(id);
                if (mem == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = JsonConvert.SerializeObject(mem);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] RegisterationRequestDTO registerRequest)
        {
            bool isUnique = _repository.IsUniqueMember(registerRequest.Email);
            if (!isUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Email already exists");
                return BadRequest(_response);
            }

            var user = await _repository.Register(registerRequest);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<APIResponse> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var mem = _repository.GetMemberById(id);
                if (mem == null)
                {
                    return NotFound();
                }
                _repository.DeleteMember(mem);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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

        [HttpPut("{id:int}")]
        public ActionResult<APIResponse> Update(int id, [FromBody] MemberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.MemberId)
                {
                    return BadRequest();
                }

                _repository.UpdateMember(updateDTO);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
