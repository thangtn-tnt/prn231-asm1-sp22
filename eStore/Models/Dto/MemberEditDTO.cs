using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Models.Dto
{
    public class MemberEditDTO
    {
        public int MemberId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
