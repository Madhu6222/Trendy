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
        ProductsService productsService = new ProductsService();

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductTable()
        {
            var products = productsService.GetProducts();

            return PartialView(products);
        }

        [HttpPost]
        public ActionResult ProductTable(string search)
        {
            var products = productsService.GetProducts();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }


            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryService categoryService = new CategoryService();

            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = categoryService.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            CategoryService categoryService = new CategoryService();

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.CategoryID = model.CategoryID;
           // newProduct.Category = categoryService.GetCategory(model.CategoryID);

            productsService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productsService.GetProduct(ID);
            
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsService.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

    }
}