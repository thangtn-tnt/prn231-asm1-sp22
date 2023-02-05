using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Models.Dto
{
    public class RegisterationRequestDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
