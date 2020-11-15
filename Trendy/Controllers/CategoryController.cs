using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trendy.Entities;
using Trendy.Services;
using Trendy.ViewModels;
using System.Data.Entity;

namespace Trendy.Controllers
{
    public class CategoryController : Controller
    {
   
        public ActionResult Index()
        {
            
            return View();
        }

        //[HttpGet]
        //public ActionResult CategoryTable()
        //{
        //    var categories = categoryService.GetCategories();

        //    return PartialView(categories);
        //}

        //[HttpPost]
        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.SearchTerm = search;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoryService.Instance.GetCategoriesCount(search);
            model.Categories = CategoryService.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories !=null)
            {

           
                model.Pager = new Pager(totalRecords, pageNo,3);

                return PartialView("CategoryTable", model);

            }

            else
            {
                return HttpNotFound();
            }
        }

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.isFeatured = model.isFeatured;

                CategoryService.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        #endregion

        #region Update

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoryService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoryService.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            CategoryService.Instance.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }

        #endregion

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            
            CategoryService.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
    }
}