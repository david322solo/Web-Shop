using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Repository;
using WebApplication7.ViewModels;
namespace WebApplication7.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        public ActionResult List(string category,string searchString,int page = 1)
        {
            IQueryable<Product> products;
            try
            {
                if (category != null)
                {
                    products = repository.ProductsOffsetCategory((page - 1) * PageSize, PageSize, category);
                }
                else
                {
                    products = repository.ProductsOffset((page - 1) * PageSize, PageSize);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            foreach (var q in products)
            {
                Console.WriteLine(q.Id);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                products = repository.FindProductByName(searchString);
            }
            
            ProductsListViewModel model = new ProductsListViewModel()
            {
                products = products,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                       repository.Count() :
                        repository.Count(category)
                },
                CurrentCategory = category
            };
            return View(model);
        }
        
    }
}