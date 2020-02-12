using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;

namespace WebApplication7.ViewModels
{
    public class DetailsViewModel
    {
        public Product Product { get; set; }
        public string ReturnUrl { get; set; }
    }
}
