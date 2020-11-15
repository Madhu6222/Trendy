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

        public List<Product> GetProductsWithID(List<int> IDs)
        {
            using (var context = new TrendyDbContext())
            {
                return context.Products.Where(product=> IDs.Contains(product.ID)).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;

            using (var context = new TrendyDbContext())
            {
                //return context.Products.OrderBy(x=>x.Name).Skip((pageNo-1)* pageSize).Take(pageSize).Include(c => c.Category).ToList();
                return context.Products.Include(c => c.Category).ToList();
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
