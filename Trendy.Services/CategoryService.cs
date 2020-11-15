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
    public class CategoryService
    {

        #region Singleton 
        private CategoryService()
        {

        }

        public static CategoryService Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoryService();

                return instance;

            }
        }
        private static CategoryService instance { get; set; }

        #endregion

        public Category GetCategory(int ID)
        {
            using (var context = new TrendyDbContext())
            {
                return context.Categories.Find(ID);
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new TrendyDbContext())
            {
                return context.Categories.Include(x=>x.Products).ToList();
            }
        }


        public List<Category> GetFeaturedCategories()
        {
            using (var context = new TrendyDbContext())
            {
                return context.Categories.Where(x => x.isFeatured && x.ImageURL!=null).ToList();
            }
        }

        public void SaveCategory(Category category) 
        {
            using (var context = new TrendyDbContext()) 
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new TrendyDbContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new TrendyDbContext())
            {
                // context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var category = context.Categories.Find(ID);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
