using AutoMapper;
using BusinessObject;
using DataAccess.Dto;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Member, MemberDTO>().ReverseMap();
                        cfg.CreateMap<Member, RegisterationRequestDTO>().ReverseMap();
                        cfg.CreateMap<MemberDTO, LoginRequestDTO>().ReverseMap();
                        cfg.CreateMap<Member, MemberUpdateDTO>().ReverseMap();
                        // Add any additional mappings here
                    });

                    _mapper = config.CreateMapper();
                }

                return _mapper;
            }
        }
        public static List<MemberDTO> GetMembers()
        {
            var listMembers = new List<MemberDTO>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listMembers = Mapper.Map<List<MemberDTO>>(context.Members.ToList());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
        public static async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            if (!IsValidLogin(loginRequest))
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    Member = null
                };
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SD.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, loginRequest.Email.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new LoginResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    Member = Mapper.Map<MemberDTO>(loginRequest)
                };
            }
        }
        public static bool IsValidLogin(LoginRequestDTO loginRequest)
        {
            try
            {
                if (IsAdmin(loginRequest))
                {
                    return loginRequest.Password.Equals(SD.DefaultAccount.Password);
                }

                using (var context = new ApplicationDbContext())
                {
                    var member = FindByEmail(loginRequest.Email);

                    return member is null || !member.Password.Equals(loginRequest.Password) ? false : true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static bool IsAdmin(LoginRequestDTO loginRequest) => SD.DefaultAccount.Email.ToLower() == loginRequest.Email.ToLower();
        public static async Task<MemberDTO> Register(RegisterationRequestDTO registerRequest)
        {
            if (FindByEmail(registerRequest.Email) == null)
            {
                Member member = Mapper.Map<Member>(registerRequest);

                try
                {
                    using (var context = new ApplicationDbContext())
                    {
                        await context.Members.AddAsync(member);

                        int result = await context.SaveChangesAsync();

                        if (result > 0)
                        {
                            var memberToReturn = context.Members.FirstOrDefault(u => u.Email == registerRequest.Email);
                            return Mapper.Map<MemberDTO>(memberToReturn);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return new MemberDTO();
        }
        public static Member FindByEmail(string email)
        {
            if (email.ToLower() == SD.DefaultAccount.Email.ToLower())
            {
                return new Member { Email = email, Password = SD.DefaultAccount.Password };
            }

            Member member = new Member();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    member = context.Members.SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return member;
        }
        public static void SaveMember(RegisterationRequestDTO member)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Members.Add(Mapper.Map<Member>(member));
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateMember(MemberUpdateDTO member)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    if (string.IsNullOrEmpty(member.Password))
                    {
                        member.Password = FindById(member.MemberId).Password;
                    }

                    context.Update(Mapper.Map<Member>(member));
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
