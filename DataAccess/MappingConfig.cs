using AutoMapper;
using BusinessObject;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
        }
    }
}
