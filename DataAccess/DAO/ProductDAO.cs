using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listProducts = context.Products.Include("Category").ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listProducts;
        }

        public static List<Category> GetCategories()
        {
            var listCate = new List<Category>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listCate = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listCate;
        }

        public static Product FindById(int prodId)
        {
            Product product = new Product();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    product = context.Products.Include("Category").SingleOrDefault(x => x.ProductId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }
        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry<Product>(product).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var prodFromDb = context.Products
                        .SingleOrDefault(x => x.ProductId == product.ProductId);

                    context.Products.Remove(prodFromDb);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
