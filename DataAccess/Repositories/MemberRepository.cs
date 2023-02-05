using AutoMapper;
using BusinessObject;
using DataAccess.DAO;
using DataAccess.Dto;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public bool IsUniqueMember(string email) => MemberDAO.FindByEmail(email) is null ? true : false;
        public void SaveMember(RegisterationRequestDTO member) => MemberDAO.SaveMember(member);
        public void DeleteMember(Member member) => MemberDAO.DeleteMember(member);
        public void UpdateMember(MemberUpdateDTO member) => MemberDAO.UpdateMember(member);
        public Member GetMemberById(int id) => MemberDAO.FindById(id);
        public List<MemberDTO> GetMembers() => MemberDAO.GetMembers();
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest) => MemberDAO.Login(loginRequest);
        public Task<MemberDTO> Register(RegisterationRequestDTO registerationRequest) => MemberDAO.Register(registerationRequest);
    }
}
