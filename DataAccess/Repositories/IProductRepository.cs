using BusinessObject;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        void SaveProduct(ProductCreateDTO product);
        ProductDTO GetProductById(int id);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        List<CategoryDTO> GetCategories();
        List<ProductDTO> GetProducts();
    }
}
