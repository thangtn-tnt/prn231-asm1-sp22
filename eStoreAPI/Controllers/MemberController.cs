using BusinessObject;
using DataAccess.Dto;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _repository = new MemberRepository();

        //GET: api/Member
        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMembers() => _repository.GetMembers();

        [HttpGet("{id}")]
        public ActionResult<Member> FindById([FromRoute] int id) => _repository.GetMemberById(id);

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _repository.Login(model);
            if (loginResponse.Member == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                //_response.StatusCode = HttpStatusCode.BadRequest;
                //_response.IsSuccess = false;
                //_response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest();
            }            
            return Ok(loginResponse);
        }

        //POST: MemberController/Member
        [HttpPost]
        public IActionResult SaveMember(Member mem)
        {
            if (_repository.IsUniqueMember(mem.Email))
            {
                return BadRequest();
            }

            _repository.SaveMember(mem);
            return NoContent();
        }

        //GET: api/Member/5
        [HttpDelete("id")]
        public IActionResult DeleteMember(int id)
        {
            var memFromDb = _repository.GetMemberById(id);

            if (memFromDb == null)
            {
                return NotFound();
            }

            _repository.DeleteMember(memFromDb);
            return NoContent();
        }
    }
}
