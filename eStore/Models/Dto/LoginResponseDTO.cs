using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Models.Dto
{
    public class LoginResponseDTO
    {
        public MemberDTO Member { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
