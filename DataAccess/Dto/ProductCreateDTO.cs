using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class ProductCreateDTO
    {      
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int UnitsInStock { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
