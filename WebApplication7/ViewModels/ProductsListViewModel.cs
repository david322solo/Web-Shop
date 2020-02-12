using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;
namespace WebApplication7.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
