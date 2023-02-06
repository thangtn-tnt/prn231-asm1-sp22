using AutoMapper;
using AutoMapper.Execution;
using BusinessObject;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Product, ProductDTO>()
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName)).ReverseMap();
                        cfg.CreateMap<Product, ProductCreateDTO>().ReverseMap();
                        cfg.CreateMap<Product, ProductUpdateDTO>().ReverseMap();
                        cfg.CreateMap<ProductDTO, ProductUpdateDTO>().ReverseMap();
                        cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                        // Add any additional mappings here
                    });

                    _mapper = config.CreateMapper();
                }

                return _mapper;
            }
        }
        public static List<ProductDTO> GetProducts()
        {
            var listProducts = new List<ProductDTO>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listProducts = Mapper.Map<List<ProductDTO>>(context.Products.Include("Category").ToList());
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listProducts;
        }
        public static List<CategoryDTO> GetCategories()
        {
            var listCate = new List<CategoryDTO>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listCate = Mapper.Map<List<CategoryDTO>>(context.Categories.ToList());
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listCate;
        }
        public static ProductDTO FindById(int prodId)
        {
            ProductDTO product = new ProductDTO();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    product = Mapper.Map<ProductDTO>(context.Products.Include("Category").SingleOrDefault(x => x.ProductId == prodId));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }
        public static void SaveProduct(ProductCreateDTO create)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    Product product = Mapper.Map<Product>(create);
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static bool CheckProductAvailable(OrderCreateDTO update)
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {
                    var product = ProductDAO.FindById(update.ProductId);

                    if (product != null && product.UnitsInStock >= update.Quantity)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }
        public static ProductDTO FindByName(string name)
        {
            ProductDTO member = new ProductDTO();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    member = Mapper.Map<ProductDTO>(context.Products.Where(u => u.ProductName.ToLower() == name));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return member;
        }
        public static void UpdateProduct(ProductUpdateDTO product)
        {

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    if (product.CategoryId == 0)
                    {
                        product.CategoryId = context.Products.AsNoTracking().SingleOrDefault(x => x.ProductId == product.ProductId).CategoryId;
                    }

                    context.Update(Mapper.Map<Product>(product));
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateQuantity(OrderCreateDTO order)
        {
            var product = ProductDAO.FindById(order.ProductId);
            if (product != null)
            {
                product.UnitsInStock -= order.Quantity;

                ProductDAO.UpdateProduct(Mapper.Map<ProductUpdateDTO>(product));
            }
        }
        public static void DeleteProduct(ProductDTO product)
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
