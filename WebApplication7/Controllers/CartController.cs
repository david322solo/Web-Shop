using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebApplication7.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication7.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        //private Cart cart;
        public CartController(IProductRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index(string returnUrl)
        {
            if (SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart") != null)
            {
                return View(new CartIndexViewModel()
                {
                    Cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart"),
                    ReturnUrl = returnUrl ?? "/"
                });
            }
            else
            {
                Cart cart = new Cart();
                SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return View(new CartIndexViewModel()
                {
                    Cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart"),
                    ReturnUrl = returnUrl ?? "/"
                });
            }
        }
        [Route("Cart/Details/{id?}")]
        public ViewResult Details(int id, string returnUrl)
        {
            return View(new DetailsViewModel()
            {
                Product = repository.FindProduct(id),
                ReturnUrl = returnUrl ?? "/"
            });
        }
        [Authorize(Roles = "user,admin")]
        public IActionResult History()
        {
            return View(repository.GetHistory(User.Identity.Name));
        }
        public RedirectToActionResult AddToCart(int Id, string returnUrl, int quantity = 1)
        {
            if (SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart") == null)
            {
                Cart cart = new Cart();
                cart.AddItem(repository.FindProduct(Id), quantity);
                SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
                cart.AddItem(repository.FindProduct(Id), quantity);
                SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public ViewResult Checkout()
        {

            var cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            return View(cart);
        }
        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public ViewResult Checkout(Cart cart,string address)
        {
            cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if(address==null)
            {
                ModelState.AddModelError("", "Input address!");
            }
            else if(address.Length>=350)
            {
                ModelState.AddModelError("", "Incorrect address!");
            }
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (cart.Lines.Count() > 10)
            {
                ModelState.AddModelError("", "Sorry, quantity of goods over 10 to order by phone");
            }
            if (ModelState.IsValid)
            {
                HttpContext.Session.Clear();
                repository.InsertOrder(cart, User.Identity.Name,address);
                return View("Completed");
            }
            else
            {
                return View(cart);
            }
        }
        public RedirectToActionResult RemoveFromCart(int Id, string returnUrl)
        {
            Product product = repository.FindProduct(Id);
            if (product != null)
            {
                if (SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart") == null)
                {
                    Cart cart = new Cart();
                    cart.RemoveLine(product);
                    SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    var cart = SessionExtensions.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
                    cart.RemoveLine(product);
                    SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}