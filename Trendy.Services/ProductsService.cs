using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendy.Database;
using Trendy.Entities;
using System.Data.Entity;

namespace Trendy.Services
{
    public class ProductsService

    {
        #region Singleton 
        private ProductsService()
        {

        }

        public static ProductsService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProductsService();

                return instance;

            }
        }
        private static ProductsService instance { get; set; }

        #endregion

        public Product GetProduct(int ID)
        {
            using (var context = new TrendyDbContext())
            {
                return context.Products.Where(x=>x.ID ==ID).Include(x=>x.Category).FirstOrDefault();
            }
        }

        public int GetProductsCount(string search)
        {
            using (var context = new TrendyDbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name != null &&
                             product.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Products.Count();
                }
            }
        }

        public List<Product> GetAllProducts()
        {

            using (var context = new TrendyDbContext())
            {
                return context.Products.ToList();
            }
        }

        public List<Product> GetProductsWithID(List<int> IDs)
        {
            using (var context = new TrendyDbContext())
            {
                return context.Products.Where(product=> IDs.Contains(product.ID)).ToList();
            }
        }

        public List<Product> GetProducts(string search, int pageNo)
        {
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new TrendyDbContext())
            {

                if (!string.IsNullOrEmpty(search))
                {

                    return context.Products.Where(product => product.Name != null &&
                             product.Name.ToLower().Contains(search.ToLower()))
                            .OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .Include(x => x.Category)
                            .ToList();

                }
                else
                {

                    return context.Products
                            .OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .Include(x => x.Category)
                            .ToList();
                }
                
            }
        }

        public void SaveProduct(Product product) 
        {
            using (var context = new TrendyDbContext()) 
            {
                //context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;


                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new TrendyDbContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int ID)
        {
            using (var context = new TrendyDbContext())
            {
                // context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
