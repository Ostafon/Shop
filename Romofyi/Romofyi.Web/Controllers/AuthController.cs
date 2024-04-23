using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Romofyi.Application;
using WebApplication1.Application;
using WebApplication1.Controllers.WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher = new PasswordHasher();
        private readonly ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        public AuthController()
        {
            _authService = new AuthService(applicationDbContext, _passwordHasher);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.AuthenticateUser(model.Username, model.Password);
                if (user != null)
                {
                    SignInUser(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.RegisterUser(model.Username, model.Password);
                if (user != null)
                {
                    SignInUser(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Registration failed.");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(authCookie);
            return RedirectToAction("Login");
        }

        private void SignInUser(Customer user)
        {
            var authTicket = new FormsAuthenticationTicket(
                1, // version
                user.Username, // user name
                DateTime.Now, // creation
                DateTime.Now.AddMinutes(30), // expiration
                false, // persistent?
                user.Role // user data
            );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }
    }
    
    namespace WebApplication1.Models
    {
        public class RegisterViewModel
        {
            [Required(ErrorMessage = "Username is required")]
            [StringLength(50, ErrorMessage = "Username must be less than 50 characters")]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required")]
            [DataType(DataType.Password)]
            [System.Web.Mvc.Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }
        }
    }
    
    namespace WebApplication1.Models
    {
        public class LoginViewModel
        {
            [Required(ErrorMessage = "Username is required")]
            [StringLength(50, ErrorMessage = "Username must be less than 50 characters")]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }

}
