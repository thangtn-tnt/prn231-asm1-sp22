using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        public static List<Member> GetMembers()
        {
            var listMembers = new List<Member>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listMembers = context.Members.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return listMembers;
        }
        public static Member FindById(int memId)
        {
            Member member = new Member();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    member = context.Members.SingleOrDefault(x => x.MemberId == memId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return member;
        }
        public static Member FindByEmail(string email)
        {
            Member member = new Member();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    member = context.Members.SingleOrDefault(x => x.Email == email);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return member;
        }
        public static void SaveMember(Member member)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Members.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateMember(Member member)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry<Member>(member).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteMember(Member member)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var prodFromDb = context.Members
                        .SingleOrDefault(x => x.MemberId == member.MemberId);

                    context.Members.Remove(prodFromDb);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
