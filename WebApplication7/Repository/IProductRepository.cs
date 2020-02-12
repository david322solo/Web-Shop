using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Product> ProductsOffset(int page, int count);
        IQueryable<Product> ProductsOffsetCategory(int page, int count,string category);
        int Count();
        int Count(string category);
        void AddRoleForUser(string id, string role);
        IQueryable<LastProductViewModel> lastProductViewModels();
        int CountLastWeek();
        void SaveProduct(Product product);
        Product FindProduct(int id);
        IQueryable<Product> FindProductByName(string name);
        IQueryable<Colore> Colores { get;}
        IQueryable<Material> Materials { get; }
        IQueryable<Sezone> Sezones { get; }
        IQueryable<Brand> Brands { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<AllHistory> Histories { get; }
        IQueryable<History> GetHistory(string Name);
        IQueryable<Role> Roles { get; }
        void InsertOrder(Cart cart, string Name,string address);
        Product DeleteProduct(int ?productID);
        Category DeleteCategory(int? categoryId);
        Colore DeleteColore(int? colore);
        void InsertColore(Colore colore);
        void InsertMaterial(Material material);
        void InsertBrand(Brand brand);
        void InsertSeasone(Sezone sezone);
        void SaveColore(Colore colore);
        void InsertCategory(Category category);
        void SaveCategory(Category category);
        void Export(string tableName);
        void Import(string tableName);
    }
}
