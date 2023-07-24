using DemoApp.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using DemoApp.Contracts;
using Microsoft.CodeAnalysis.Scripting;

namespace DemoApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public AccountController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(SignInModel model)
        {
            if (ValidateUser(model.UserName, model.Password))
            {
                // Successful login: Create and store user data in the session
                HttpContext.Session.SetString("UserName", model.UserName);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public IActionResult Logout()
        {
            // Log out the user: Remove user data from the session
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        private bool ValidateUser(string userName, string password)
        {

            var existingUser = _repository.User.FindBy(x => x.Username == userName).FirstOrDefault();
            if (existingUser != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(password, existingUser.HashedPassword);
              
                return verified;
            }

            return false;
        }
    }
}
