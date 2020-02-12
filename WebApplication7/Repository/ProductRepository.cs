using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;
using WebApplication7.DataBase;
using WebApplication7.ViewModels;

namespace WebApplication7.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DB db = new DB();
        public IQueryable<Product> Products
        {
            get { return db.Products; }
        }
        public IQueryable<Product> ProductsOffset(int page, int count)
        {
            return db.Offset(page, count);
        }
        public IQueryable<LastProductViewModel> lastProductViewModels()
        {
            return db.LastProduct();
        }
        public void AddRoleForUser(string id, string role)
        {
             db.AddRole(id, role);
        }
        public IQueryable<Product> ProductsOffsetCategory(int page, int count,string category)
        {
            return db.OffsetCategory(page, count,category);
        }
        public Product FindProduct(int id)
        {
            return db.FindProduct(id);
        }
        public IQueryable<Product> FindProductByName(string name)
        {
            return db.FindProductByName(name);
        }
        public int Count()
        {
            return db.Count();
        }
        public int CountLastWeek()
        {
            return db.CountLastWeek();
        }
        public int Count(string category)
        {
            return db.Count(category);
        }
        public IQueryable<Colore> Colores
        {
            get { return db.Colors; }
        }
        public IQueryable<AllHistory> Histories
        {
            get { return db.Histories; }
        }
        public IQueryable<Material> Materials
        {
            get { return db.Materials; }
        }
        public IQueryable<Sezone> Sezones
        {
            get { return db.Sezones; }
        }
        public IQueryable<Brand> Brands
        {
            get { return db.Brands; }
        }
        public IQueryable<Category> Categories
        {
            get { return db.Categories; }
        }
        public Product DeleteProduct(int? productID)
        {
            db.Delete(productID);
            return new Product();
        }
        public IQueryable<History> GetHistory(string Name)
        {
            return db.GetHistory(Name);
        }
        public void InsertCategory(Category category)
        {
            db.AddCategory(category);
        }
        public void Export(string tableName)
        {
            db.Export(tableName);
        }
        public void Import(string tableName)
        {
            db.Import(tableName);
        }

        public void InsertOrder(Cart cart, string Name, string address)
        {
            db.InsertOrder(cart, Name, address);
        }
        public IQueryable<Role> Roles
        {
            get
            {
                return db.Roles;
            }
        }
        public Category DeleteCategory(int? categoryId)
        {
            db.DeleteCat(categoryId);
            return new Category();
        }
        public void SaveCategory(Category category)
        {
                db.UpdateCategory(category);
        }
        public void InsertColore(Colore colore)
        {
            db.AddColore(colore);
        }
        public void InsertBrand(Brand brand)
        {
            db.AddBrand(brand);
        }
        public void InsertSeasone(Sezone sezone)
        {
            db.AddSezone(sezone);
        }
        public void SaveColore(Colore colore)
        {
            db.UpdateColore(colore);
        }
        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                db.Add(product);
            }
            else
            {
                db.UpdateProduct(product);
            }
        }

        public Colore DeleteColore(int? colore)
        {
            db.DeleteColore(colore);
            return new Colore();
        }
        public void InsertMaterial(Material material)
        {
            db.AddMaterial(material);
        }
    }
}
