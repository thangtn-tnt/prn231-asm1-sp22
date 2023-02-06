using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BusinessObject
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }     
        public int CategoryId { get; set; }
        public string ProductName { get; set; }        
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }        
        public Category? Category { get; set; }
    }
}
