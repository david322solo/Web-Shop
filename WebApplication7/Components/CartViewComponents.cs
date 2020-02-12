using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;
using WebApplication7.Repository;
namespace WebApplication7.Components
{
    public class CartViewComponent : ViewComponent
    {
        public CartViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
            var cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if (cart != null)
            {

                ViewBag.Count = cart.Lines.Sum(q => q.Quantity);
                ViewBag.Total = cart.ComputeTotalValue();
            }
            else
            {
                ViewBag.Count = 0;
                ViewBag.Total = 0;
            }
            return View();
        }
    }
}
