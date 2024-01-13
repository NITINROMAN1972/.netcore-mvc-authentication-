using Microsoft.AspNetCore.Mvc;using System.Security.Claims;using Microsoft.AspNetCore.Authentication;using Microsoft.AspNetCore.Authentication.Cookies;using EPMS.Models;namespace EPMS.Controllers{    public class AccessController : Controller    {        public IActionResult Login()        {
            // checking if user is still logged in
            ClaimsPrincipal claimUser = HttpContext.User;            if(claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }            return View();        }        [HttpPost]        public async Task<IActionResult> Login(VMLogin modelLogin)        {            if(modelLogin.Email == "milind@gmail.com" && modelLogin.Password == "101")
            {
                List<Claim> claims = new List<Claim>()
                {
                    // creating a new claim
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Eg - Role, for role based authorization")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }            ViewData["ValidateMessage"] = "User not found !";            return View();        }    }}