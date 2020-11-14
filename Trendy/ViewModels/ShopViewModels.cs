using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trendy.Entities;

namespace Trendy.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
    }
}