using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication7.Models;
using WebApplication7.Repository;
using System.Text.Json;
using System.Reflection;
using Newtonsoft.Json;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public int PageSize = 20;
        IProductRepository repository;
        IUserRepository userRepository;
        public AdminController(IProductRepository repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }
        // GET: Admin

        public ActionResult Index(string table, int page = 1)
        {
            if (table != null)
            {
                table = table.ToLower();
                switch (table)
                {
                    case "product":
                        var pre = repository.ProductsOffset((page - 1) * PageSize, PageSize);
                        ProductsListViewModel model = new ProductsListViewModel()
                        {
                            products = pre,
                            PagingInfo = new PagingInfo()
                            {
                                CurrentPage = page,
                                ItemsPerPage = PageSize,
                                TotalItems = repository.Count()
                            },
                        };
                        return View("IndexProduct", model);
                    case "user":
                        return View("IndexUser", userRepository.Users.OrderBy(p => p.Id));
                    case "category":
                        return View("IndexCategory", repository.Categories.OrderBy(p => p.Id));
                    case "colore":
                        return View("IndexColore", repository.Colores.OrderBy(p => p.Id));
                    case "history":
                        return View("IndexHistory", repository.Histories.OrderBy(p => p.dateTime));
                    case "material":
                        return View("IndexMaterial", repository.Materials.OrderBy(p => p.Id));
                    case "brand":
                        return View("IndexBrand", repository.Brands.OrderBy(p => p.Id));
                    case "role":
                        return View("IndexRole", repository.Roles.OrderBy(p => p.Id));
                    case "seasone":
                        return View("IndexSeasone", repository.Sezones.OrderBy(p => p.Id));
                    default:
                        return NotFound();
                }
            }
            else
            {
                return NotFound();

            }
        }
        public IActionResult Export(string tb)
        {
            try
            {
                repository.Export(tb);
                TempData["message"] = string.Format("Table is exported");
                if(tb=="usercustomer")
                {
                    tb = "user";
                }
                if (tb == "Sezone")
                {
                    tb = "seasone";
                }
                return RedirectToAction(nameof(Index), new { table = tb });

            }
            catch (Exception ex)
            {
                TempData["message"] = string.Format("Can't export");
                Console.WriteLine(ex.Message);
                if (tb == "usercutomer")
                {
                    tb = "user";
                }
                if (tb == "Sezone")
                {
                    tb = "seasone";
                }
                return RedirectToAction(nameof(Index), new { table = tb });

            }
        }
        public IActionResult Import(string tb)
        {
            try
            {
                repository.Import(tb);
                TempData["message"] = string.Format("Table is imported");
                if (tb == "usercustomer")
                {
                    tb = "user";
                }
                if (tb == "Sezone")
                {
                    tb = "seasone";
                }
                return RedirectToAction(nameof(Index), new { table = tb });

            }
            catch (Exception ex)
            {
                TempData["message"] = string.Format("Can't imported");
                Console.WriteLine(ex.Message);
                if (tb == "usercutomer")
                {
                    tb = "user";
                }
                if (tb == "Sezone")
                {
                    tb = "seasone";
                }
                return RedirectToAction(nameof(Index), new { table = tb });

            }
        }
        public IActionResult EditCategory(int? id)
        {
            if (id != null)
            {
                Category category = repository.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }
        public IActionResult Home()
        {
            ViewBag.RegisterUser = userRepository.Users.Count();
            ViewBag.Orders = repository.Histories.Count();
            ViewBag.Count = repository.Count();
            ViewBag.CountLastWeek = repository.CountLastWeek();
            List<LastProductViewModel> lastProductViewModel = repository.lastProductViewModels().ToList();
            return View(lastProductViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            try
            {
                if (category.Name == null)
                {
                    ModelState.AddModelError("", "Input Name");
                }

                if (ModelState.IsValid)
                {
                    repository.SaveCategory(category);
                    return RedirectToAction(nameof(Index), "Category");
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            Category cat = repository.Categories.FirstOrDefault(c => c.Name == category.Name);
            if (cat != null)
            {

                ModelState.AddModelError("", "This category is created");
                return View();
            }
            else
            {
                repository.InsertCategory(category);
                return RedirectToAction(nameof(Index), new { table = "Category" });
            }
        }
        [HttpGet]
        [ActionName("DeleteCategory")]
        public ActionResult ConfirmDeleteCategory(int? id)
        {
            if (id != null)
            {
                Category category = repository.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int? id)
        {
            if (id != null)
            {
                Category category = repository.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    repository.DeleteCategory(category.Id);
                    return RedirectToAction(nameof(Index), "Category");
                }
            }
            return NotFound();
        }

        public IActionResult AddRole(string idr, string roler)
        {
            try
            {
                TempData["message"] = string.Format("Role added for user {0}", idr);
                repository.AddRoleForUser(idr, roler);
                return RedirectToAction(nameof(Index), new { table = "User" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["message"] = string.Format("Role can't added {0}", idr);
                return RedirectToAction(nameof(Index), new { table = "User" });
            } 
        }

        public IActionResult EditColore(int? id)
        {
            if (id != null)
            {
                Colore colore = repository.Colores.FirstOrDefault(c => c.Id == id);
                if (colore != null)
                    return View(colore);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditColore(Colore colore)
        {
            try
            {
                if (colore.NameColore == null)
                {
                    ModelState.AddModelError("", "Input Name");
                }

                if (ModelState.IsValid)
                {
                    repository.SaveColore(colore);
                    return RedirectToAction(nameof(Index), "Colore");
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateColore(Colore colore)
        {
            Colore col = repository.Colores.FirstOrDefault(c => c.NameColore == colore.NameColore);
            if (col != null)
            {
                ModelState.AddModelError("", "This colore is created");
                return View();
            }
            else
            {
                repository.InsertColore(colore);
                return RedirectToAction(nameof(Index), new { table = "Colore" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(Brand brand)
        {
            Brand br = repository.Brands.FirstOrDefault(c => c.Name == brand.Name);
            if (br != null)
            {
                ModelState.AddModelError("", "This brand is created");
                return View();
            }
            else
            {
                repository.InsertBrand(brand);
                return RedirectToAction(nameof(Index), new { table = "Brand" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMaterial(Material material)
        {
            Material mat = repository.Materials.FirstOrDefault(c => c.Name == material.Name);
            if (mat != null)
            {
                ModelState.AddModelError("", "This material is created");
                return View();
            }
            else
            {
                repository.InsertMaterial(material);
                return RedirectToAction(nameof(Index), new { table = "Material" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeasone(Sezone sezone)
        {
            Sezone sez = repository.Sezones.FirstOrDefault(c => c.Name == sezone.Name);
            if (sez != null)
            {
                ModelState.AddModelError("", "This seasone is created");
                return View();
            }
            else
            {
                repository.InsertSeasone(sezone);
                return RedirectToAction(nameof(Index), new { table = "Seasone" });
            }
        }

        [HttpGet]
        [ActionName("DeleteColore")]
        public ActionResult ConfirmDeleteColore(int? id)
        {
            if (id != null)
            {
                Colore colore = repository.Colores.FirstOrDefault(c => c.Id == id);
                if (colore != null)
                    return View(colore);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteColore(int? id)
        {
            if (id != null)
            {
                Colore colore = repository.Colores.FirstOrDefault(c => c.Id == id);
                if (colore != null)
                {
                    if (repository.Products.FirstOrDefault(p => p.NameColore == colore.NameColore) != null)
                    {
                        ModelState.AddModelError("", "Can't delete because this colore have in product");
                        return View();
                    }
                    repository.DeleteColore(colore.Id);
                    return RedirectToAction(nameof(Index), "Colore");
                }
            }
            return NotFound();
        }
        #region
        //public ActionResult CreateUser()
        //{
        //    return View(new User() { Roles = repository.Roles.ToList()});
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateUser(User user)
        //{
        //    try
        //    {
        //        if (product.Name == null)
        //        {
        //            ModelState.AddModelError("", "Input Name");
        //        }
        //        if (product.Description == null)
        //        {
        //            ModelState.AddModelError("", "Input Descriptions");
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            repository.SaveProduct(product);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            IEnumerable<Colore> colores = repository.Colores;
        //            IEnumerable<Material> materials = repository.Materials;
        //            IEnumerable<Sezone> sezones = repository.Sezones;
        //            IEnumerable<Brand> brands = repository.Brands;
        //            IEnumerable<Category> categories = repository.Categories;

        //            ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
        //            ViewBag.Materials = new SelectList(materials, "Name", "Name");
        //            ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
        //            ViewBag.Brands = new SelectList(brands, "Name", "Name");
        //            ViewBag.Categories = new SelectList(categories, "Name", "Name");
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return View();
        //    }
        //}
        #endregion
        public IActionResult EditUser(int? id)
        {
            IEnumerable<Colore> colores = repository.Colores;
            IEnumerable<Material> materials = repository.Materials;
            IEnumerable<Sezone> sezones = repository.Sezones;
            IEnumerable<Brand> brands = repository.Brands;
            IEnumerable<Category> categories = repository.Categories;

            ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
            ViewBag.Materials = new SelectList(materials, "Name", "Name");
            ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
            ViewBag.Brands = new SelectList(brands, "Name", "Name");
            ViewBag.Categories = new SelectList(categories, "Name", "Name");
            if (id != null)
            {
                Product product = repository.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }

        
        //POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int? id)
        {
            if (id != null)
            {
                Product product = repository.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    repository.DeleteProduct(product.Id);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        // GET: Admin/Create
        public ActionResult Create(string table)
        {
            if (table != null)
            {
                table = table.ToLower();
                ViewBag.Table = table;
                switch (table)
                {
                    case "product":
                        IEnumerable<Colore> colores = repository.Colores;
                        IEnumerable<Material> materials = repository.Materials;
                        IEnumerable<Sezone> sezones = repository.Sezones;
                        IEnumerable<Brand> brands = repository.Brands;
                        IEnumerable<Category> categories = repository.Categories;

                        ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
                        ViewBag.Materials = new SelectList(materials, "Name", "Name");
                        ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
                        ViewBag.Brands = new SelectList(brands, "Name", "Name");
                        ViewBag.Categories = new SelectList(categories, "Name", "Name");
                        return View("CreateProduct", new Product());
                    case "category":
                        return View("CreateCategory", new Category());
                    case "colore":
                        return View("CreateColore", new Colore());
                    case "material":
                        return View("CreateMaterial", new Material());
                    case "brand":
                        return View("CreateBrand", new Brand());
                    case "seasone":
                        return View("CreateSeasone", new Sezone());
                    default:
                        return NotFound();
                }
            }
            return NotFound();

        }

        // POST: Admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CreateProduct")]
        public ActionResult CreateProduct(Product product, IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                    {
                        product.ImageData = binaryReader.ReadBytes((int)image.Length);
                    }
                    product.ImageMimeType = image.ContentType;
                }
                else
                {
                    ModelState.AddModelError("", "Please select photo");
                }
                if (product.Description == null)
                {
                    ModelState.AddModelError("", "Input Descriptions");
                }
                if (ModelState.IsValid)
                {
                    repository.SaveProduct(product);
                    TempData["message"] = string.Format("{0} has been saved", product.Name);
                    return RedirectToAction("Index", new { table = "Product" });
                }
                else
                {
                    IEnumerable<Colore> colores = repository.Colores;
                    IEnumerable<Material> materials = repository.Materials;
                    IEnumerable<Sezone> sezones = repository.Sezones;
                    IEnumerable<Brand> brands = repository.Brands;
                    IEnumerable<Category> categories = repository.Categories;

                    ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
                    ViewBag.Materials = new SelectList(materials, "Name", "Name");
                    ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
                    ViewBag.Brands = new SelectList(brands, "Name", "Name");
                    ViewBag.Categories = new SelectList(categories, "Name", "Name");
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                IEnumerable<Colore> colores = repository.Colores;
                IEnumerable<Material> materials = repository.Materials;
                IEnumerable<Sezone> sezones = repository.Sezones;
                IEnumerable<Brand> brands = repository.Brands;
                IEnumerable<Category> categories = repository.Categories;

                ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
                ViewBag.Materials = new SelectList(materials, "Name", "Name");
                ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
                ViewBag.Brands = new SelectList(brands, "Name", "Name");
                ViewBag.Categories = new SelectList(categories, "Name", "Name");
                return View(product);
            }
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(string table, int? id)
        {
            IEnumerable<Colore> colores = repository.Colores;
            IEnumerable<Material> materials = repository.Materials;
            IEnumerable<Sezone> sezones = repository.Sezones;
            IEnumerable<Brand> brands = repository.Brands;
            IEnumerable<Category> categories = repository.Categories;

            ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
            ViewBag.Materials = new SelectList(materials, "Name", "Name");
            ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
            ViewBag.Brands = new SelectList(brands, "Name", "Name");
            ViewBag.Categories = new SelectList(categories, "Name", "Name");
            if (id != null)
            {
                Product product = repository.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                    return View("EditProduct", product);
                return NotFound();
            }
            return NotFound();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product, IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                    {
                        product.ImageData = binaryReader.ReadBytes((int)image.Length);
                    }
                    product.ImageMimeType = image.ContentType;
                }
                if (product.Name == null)
                {
                    ModelState.AddModelError("", "Input Name");
                }

                if (product.Description == null)
                {
                    ModelState.AddModelError("", "Input Descriptions");
                }
                if (ModelState.IsValid)
                {
                    repository.SaveProduct(product);
                    TempData["message"] = string.Format("{0} has been modified", product.Name);
                    return RedirectToAction(nameof(Index), new { table = "Product" });
                }
                else
                {
                    IEnumerable<Colore> colores = repository.Colores;
                    IEnumerable<Material> materials = repository.Materials;
                    IEnumerable<Sezone> sezones = repository.Sezones;
                    IEnumerable<Brand> brands = repository.Brands;
                    IEnumerable<Category> categories = repository.Categories;

                    ViewBag.Colores = new SelectList(colores, "NameColore", "NameColore");
                    ViewBag.Materials = new SelectList(materials, "Name", "Name");
                    ViewBag.Sezones = new SelectList(sezones, "Name", "Name");
                    ViewBag.Brands = new SelectList(brands, "Name", "Name");
                    ViewBag.Categories = new SelectList(categories, "Name", "Name");
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(product);
            }
        }

        // GET: Admin/Delete/5
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Product product = repository.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                    return View("DeleteProduct", product);
            }
            return NotFound();
        }

        //POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Product product = repository.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    repository.DeleteProduct(product.Id);
                    if (repository.Products.FirstOrDefault(p => p.Id == id) == null)
                    {

                        TempData["message"] = string.Format("{0} has been deleted", product.Name);
                        return RedirectToAction(nameof(Index), new { table = "Product" });
                    }
                    else
                    {
                        TempData["message"] = string.Format("can't delete product {0}, because product has in history. Can make product no visible", product.Name);
                        Console.WriteLine("Error adding");
                        return RedirectToAction(nameof(Index), new { table = "Product" });
                    }
                }
            }
            return NotFound();
        }
    }

}
