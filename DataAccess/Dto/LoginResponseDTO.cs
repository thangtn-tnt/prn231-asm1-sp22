using BusinessObject;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class LoginResponseDTO
    {
        public Member Member { get; set; }
        public string Token { get; set; }
    }
}
