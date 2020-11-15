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

        //public int GetCategoriesCount()
        //{
        //    using (var context = new TrendyDbContext())
        //    {
        //        return context.Categories.Count();

        //    }
        //}

        public int GetCategoriesCount(string search)
        {
            using (var context = new TrendyDbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                             category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        public List<Category> GetAllCategories()
        {

            using (var context = new TrendyDbContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetCategories(string search,int pageNo)
        {
            int pageSize = 3; //int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new TrendyDbContext())
            {

                if (!string.IsNullOrEmpty(search))
                {

                    return context.Categories.Where(category => category.Name != null && 
                             category.Name.ToLower().Contains(search.ToLower()))
                            .OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .Include(x=>x.Products)
                            .ToList();

                }
                else
                {

                    return context.Categories
                            .OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .Include(x=>x.Products)
                            .ToList();
                }
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
