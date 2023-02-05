using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Models.Dto
{
    public class ProductUpdateDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Weight { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int CategoryId { get; set; }
    }
}
