using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        public string NameSezone { get; set; }
        public string NameColore { get; set; }
        public string NameMaterial { get; set; }
        public string NameBrand { get; set; }
        public string Category { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set ; }
        [Display(Name="Photo")]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
