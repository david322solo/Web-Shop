using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Repository;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository repository;
        public AccountController(IUserRepository userRepository)
        {
            repository = userRepository;

        }
        public static string Query = null;
        public ActionResult Login()
        {
            Query = Request.Query["ReturnUrl"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = repository.Users.FirstOrDefault(u => u.Login == loginModel.Login && u.Password == WebApplication7.HashPassword.HashSha256.ComputeSha256(loginModel.Password));
                if (user != null)
                {
                    Authenticate(user);
                    if (Query == null)
                    {
                        return RedirectToAction("List", "Product");
                    }
                    else 
                    {
                        String [] q = Query.Split("/");
                        if(q.Length==4)
                        {
                            return RedirectToAction(q[2], q[1], new { table= q[3] });

                        } if(q.Length==5)
                        {
                            return RedirectToAction(q[2], q[1], new { table = q[3],id=q[4] });
                        }
                        else
                        {
                            return RedirectToAction(q[2], q[1]);

                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(loginModel);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel user)
        {
            if (repository.Users.FirstOrDefault(u => u.Login == user.Login)!=null)
            {
                ModelState.AddModelError("", "A user with this login exists");
            }
            if(user.Password!=user.Confirm)
            {
                ModelState.AddModelError("", "Passwords do not match");
            }
            if (repository.Users.FirstOrDefault(u => u.Email == user.Email) != null)
            {
                ModelState.AddModelError("", "A user with this email exists");
            }
            if (ModelState.IsValid)
            {
                repository.SaveUser(new User()
                {
                    Login = user.Login,
                    Email = user.Email,
                    City = user.City,
                    Password = WebApplication7.HashPassword.HashSha256.ComputeSha256(user.Password),
                    Country = user.Country,
                    Name = user.Name,
                    Phone = user.Phone,
                    Region = user.Region
                });
                Authenticate(repository.Users.FirstOrDefault(u=>u.Login == user.Login));
                return RedirectToAction("List", "Product");
            }
            else
            {
                return View(user);
            }
            
         }
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("List", "Product");
        }
        [Authorize]
        public IActionResult Information()
        {
            User user = repository.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
            ViewBag.Name = user.Name;
            ViewBag.Login = user.Login;
            ViewBag.Email = user.Email;
            ViewBag.City = user.City;
            return View();
        }
        private void Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            };
            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.RoleName));
            }
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


    }
}