using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(Member member) => MemberDAO.DeleteMember(member);

        public Member GetMemberById(int id) => MemberDAO.FindById(id);
        public Member GetMemberByEmail(string email) => MemberDAO.FindByEmail(email);

        public List<Member> GetMembers() => MemberDAO.GetMembers();

        public void SaveMember(Member member) => MemberDAO.SaveMember(member);

        public void UpdateMember(Member member) => MemberDAO.UpdateMember(member);
    }
}
