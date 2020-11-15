using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trendy.Entities;
using Trendy.Services;
using Trendy.ViewModels;

namespace Trendy.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult ProductTable()
        //{
        //    var products = ProductsService.Instance.GetProducts();

        //    return PartialView(products);
        //}

        //[HttpPost]
        public ActionResult ProductTable(string search, int? pageNo)
        {

            ProductSearchViewModel model = new ProductSearchViewModel();

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Products = ProductsService.Instance.GetProducts(model.PageNo);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }


            return PartialView(model);
        }

        #region Create

        [HttpGet]
        public ActionResult Create()
        {

            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = CategoryService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {


            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.CategoryID = model.CategoryID;
            newProduct.ImageURL = model.ImageURL;
           // newProduct.Category = categoryService.GetCategory(model.CategoryID);

            ProductsService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        #endregion

        #region Update
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsService.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoryService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductsService.Instance.UpdateProduct(existingProduct);
            return RedirectToAction("ProductTable");
        }
        
        #endregion

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

    }
}