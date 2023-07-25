using DemoApp.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using DemoApp.Contracts;
using Microsoft.CodeAnalysis.Scripting;
using DemoApp.Repository;
using Microsoft.Extensions.Options;

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
    
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(SignInModel model)
        {
            string userID=string.Empty;
            if (ValidateUser(model.UserName, model.Password ,out userID))
            {

                var sharedUserData = new
                {
                    model.UserName,
                    UserId = userID
                };

                // Convert the shared object to JSON
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(sharedUserData);

                var cookieOptions = new CookieOptions();
            
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                cookieOptions.Path = "/";

                Response.Cookies.Append("SessionUserData", jsonData, cookieOptions);
            

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
     

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                return View(model);
            }
           
        }
        public IActionResult SignOut()
        {
            // Log out the user: Remove user data from the session
            Response.Cookies.Delete("SessionUserData");
            return RedirectToAction("Login");
        }
        private bool ValidateUser(string userName, string password,  out string userId)
        {
            userId=string.Empty;
            var existingUser = _repository.User.FindBy(x => x.Username == userName).FirstOrDefault();
            if (existingUser != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(password, existingUser.HashedPassword);
                userId = existingUser.Id.ToString();
              
                return verified;
            }

            return false;
        }
    }
}
