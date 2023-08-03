using DemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoApp.Web.Controllers
{
    public class BaseController : Controller
    {
        private SharedUserModel _user;
        protected void CreateSessionCookie(string userName, string userId)
        {
            // Create the shared object directly within the JSON serialization
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                UserName = userName,
                UserId = userId
            });

            // Set the cookie directly without the need for a separate CookieOptions variable
            Response.Cookies.Append("SessionUserData", jsonData, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                Path = "/"
            });
        }

        private SharedUserModel ReadSharedUserInfoFromCookie()
        {
            try
            {
                // Read the cookie value
                string jsonData = Request.Cookies["SessionUserData"];

                if (!string.IsNullOrEmpty(jsonData))
                {
                    // Deserialize the JSON data to the object
                    return JsonConvert.DeserializeObject<SharedUserModel>(jsonData);
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
