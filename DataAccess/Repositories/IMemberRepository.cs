using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMemberRepository
    {
        void SaveMember(Member member);
        Member GetMemberById(int id);
        bool IsUniqueMember(string email);
        void DeleteMember(Member member);
        void UpdateMember(Member member);        
        List<Member> GetMembers();
    }
}
