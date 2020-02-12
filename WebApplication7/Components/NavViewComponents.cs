using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Repository;
namespace WebApplication7.Components
{
    public class NavViewComponent : ViewComponent
    {
        IProductRepository repository;
        public NavViewComponent(IProductRepository repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            Console.WriteLine(Request.Path);
            string[] category = Request.Path.ToString().Split("/");
            ViewBag.SelectedCategory = category[category.Length - 1];   
            IEnumerable<string> categories = repository.Categories
            .Select(x => x.Name)
            .Distinct()
            .OrderBy(x => x);
            return View(categories);

        }
    }
}
