using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trendy.Services;
using Trendy.ViewModels;

namespace Trendy.Controllers
{
    public class ShopController : Controller
    {
        ProductsService productService = new ProductsService();
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var cartProductsCookie = Request.Cookies["CartProducts"];

            if (cartProductsCookie!=null)
            {
                model.CartProductIDs = cartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = productService.GetProductsWithID(model.CartProductIDs);
            }

            return View(model);
        }
    }
}