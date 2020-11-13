using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trendy.Services;
using Trendy.ViewModels;

namespace Trendy.Controllers
{
    public class HomeController : Controller
    {
        CategoryService categoryService = new CategoryService();

        public ActionResult Index()
        {
            HomeViewModels model = new HomeViewModels();

            model.FeaturedCategories = categoryService.GetFeaturedCategories();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}