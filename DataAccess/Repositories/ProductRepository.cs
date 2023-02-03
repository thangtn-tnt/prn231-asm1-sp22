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
        public void SaveProduct(Product product) => ProductDAO.SaveProduct(product);
        public Product GetProductById(int id) => ProductDAO.FindById(id);
        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);
        public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);
        public List<Category> GetCategories() => ProductDAO.GetCategories();
        public List<ProductDTO> GetProducts() => ProductDAO.GetProducts();
    }
}
