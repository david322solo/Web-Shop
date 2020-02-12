using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;
using WebApplication7.Repository;
namespace WebApplication7.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public LoginViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
