using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.ViewModels
{
    public class LastProductViewModel
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string NameBrand { get; set; }
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        
    }
}
