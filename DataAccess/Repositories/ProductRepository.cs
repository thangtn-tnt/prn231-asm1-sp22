using BusinessObject;
using DataAccess.DAO;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void SaveProduct(ProductCreateDTO product) => ProductDAO.SaveProduct(product);
        public ProductDTO GetProductById(int id) => ProductDAO.FindById(id);
        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);
        public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);
        public List<CategoryDTO> GetCategories() => ProductDAO.GetCategories();
        public List<ProductDTO> GetProducts() => ProductDAO.GetProducts();
    }
}
