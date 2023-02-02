using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        //POST: MemberController/Member
        [HttpPost]
        public IActionResult SaveMember(Member mem)
        {
            Member email = _repository.GetMemberByEmail(mem.Email);

            if (email != null)
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
