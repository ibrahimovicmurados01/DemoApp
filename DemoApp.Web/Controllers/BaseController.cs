using DemoApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoApp.Web.Controllers
{
    public class BaseController : Controller
    {
        private SharedUserModel _user;
        protected void CreateSessionCookie(string userName, string userId)
        {
      
            // Set the cookie directly without the need for a separate CookieOptions variable
            CookieOptions options = new CookieOptions
            {
               // Domain = "azurewebsites.net", // Set the domain to "localhost".
                Path = "/", // Set the path to a common path accessible by both applications.
                Expires = DateTime.Now.AddDays(1), // Set an appropriate expiration date.
                Secure = true, // Set to true if running on HTTPS.
                HttpOnly = true, // Recommended for security reasons.
                SameSite = SameSiteMode.Lax // Recommended to prevent CSRF attacks.

            };

            Response.Cookies.Append("Session_UserName", userName, options);
            Response.Cookies.Append("Session_UserId", userId, options);


            // Create the shared object directly within the JSON serialization
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                UserName = userName,
                UserId = userId
            });

        }


        private SharedUserModel ReadSharedUserInfoFromCookie()
        {
            try
            {
                // Read the cookie value
                string userName = Request.Cookies["Session_UserName"];
                string userId = Request.Cookies["Session_UserId"];
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userName))
                {
       
                    return new SharedUserModel
                    {
                        UserId = Guid.Parse(userId),
                        UserName = userName,
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, such as logging or providing a default behavior.
                // For now, we simply ignore the exception and return null.
            }

            return null;
        }

        public virtual SharedUserModel UserFromCookie
        {
            get
            {
                if (_user != null)
                {
                    return _user;
                }

                _user = ReadSharedUserInfoFromCookie();

                return _user;
            }
        }
    }
}
