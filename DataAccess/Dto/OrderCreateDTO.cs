using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class OrderCreateDTO
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }        
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
