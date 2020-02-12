using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Repository;
namespace WebApplication7.Components
{
    public class MenuViewComponent : ViewComponent
    {
        IProductRepository repository;
        public MenuViewComponent(IProductRepository repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            Console.WriteLine(Request.Path);
            string[] category = Request.Path.ToString().Split("/");
            if (category.Length == 4)
            {
                ViewBag.SelectedCategory = category[category.Length - 1];
            }
            else 
            {
                ViewBag.SelectedCategory = category[category.Length - 2];
            }
                string[] MenuTable = {"Product","User", "Category","Colore", "History",
                "Material", "Brand", "Role", "Seasone"};
            return View(MenuTable);

        }
    }
}
