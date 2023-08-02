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
using DemoApp.Entities.Models;
using System.Security.Cryptography.Xml;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DemoApp.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AccountController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            UserModel userModel = null;
            // Get the user information from the cookie
            var sharedUser = UserFromCookie;
            if (sharedUser != null)
            {
                // Retrieve information associated with the current user from the database
                var user =  _repository.User.FindBy(x => x.Id == sharedUser.UserId).FirstOrDefault();

                // Map the contacts to ContactModel using the _mapper
                userModel = _mapper.Map<UserModel>(user);
            }
            return View(userModel);
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(SignInModel model)
        {
            if (ValidateUser(model.UserName, model.Password, out string userID))
            {
                CreateSessionCookie(model.UserName, userID);
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
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueUsername = GenerateUniqueUsername(model.FirstName, model.LastName);

                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Email = model.Email,
                    Username = uniqueUsername
                };

                var registeredUser = await _repository.User.CreateAsync(user);
                await _repository.SaveAsync();

                if (registeredUser != null)
                {
                    CreateSessionCookie(registeredUser.Username, registeredUser.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "An error occurred while signing up. Please try again later.");
            }

            // If ModelState is not valid, return the same view with validation errors
            return View(model);
        }

        public IActionResult SignOut()
        {
            // Clear user data from the session by deleting the cookie
            Response.Cookies.Delete("SessionUserData");

            // Redirect to the "Login" action
            return RedirectToAction("Login");
        }

        private bool ValidateUser(string userName, string password, out string userId)
        {
            userId = null;

            // Ensure userName is not null or empty
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            // Find the user based on the search criteria
            User existingUser = _repository.User.FindBy(x => x.Email == userName || x.Username == userName).SingleOrDefault();

            // If a user is found, verify the password
            if (existingUser != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(password, existingUser.HashedPassword);
                userId = existingUser.Id.ToString();
                return verified;
            }

            return false;
        }
             

        // Generate a unique username based on the first name and last name
        private string GenerateUniqueUsername(string firstName, string lastName)
        {
            
            string baseUsername = $"{firstName.ToLower()}_{lastName.ToLower()}";
            string uniqueUsername = baseUsername;

            int counter = 1;
            while (_repository.User.FindBy(u => u.Username == uniqueUsername).FirstOrDefault() != null)
            {
                uniqueUsername = $"{baseUsername}_{counter}";
                counter++;
            }

            return uniqueUsername;
        }
    }

}
