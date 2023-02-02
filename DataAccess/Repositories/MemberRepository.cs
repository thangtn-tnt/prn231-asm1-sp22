using BusinessObject;
using DataAccess.DAO;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public bool IsUniqueMember(string email) => MemberDAO.FindByEmail(email);
        public void SaveMember(Member member) => MemberDAO.SaveMember(member);
        public void DeleteMember(Member member) => MemberDAO.DeleteMember(member);
        public void UpdateMember(Member member) => MemberDAO.UpdateMember(member);
        public Member GetMemberById(int id) => MemberDAO.FindById(id);
        public List<Member> GetMembers() => MemberDAO.GetMembers();
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest) => MemberDAO.Login(loginRequest);
        public Task<RegisterationRequestDTO> Register(RegisterationRequestDTO registerationRequest)
        {
            throw new Exception();
        }
    }
}
