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
        [DisplayName("Category Id")]
        public int CategoryId { get; set; }
        [DisplayName("Product")]
        public string ProductName { get; set; }        
        public double Weight { get; set; }
        [DisplayName("Unit Price")]
        public int UnitPrice { get; set; }
        [DisplayName("Units In Stock")]
        public int UnitsInStock { get; set; }        
        public Category? Category { get; set; }
    }
}
