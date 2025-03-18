using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl=ReturnUrl,

            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.Name);
                if (user != null)
                {
                    //oturum açma 
                    await _signInManager.SignOutAsync();
                    if((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("Error", "Invalid username or password.");
            }

            
            return View();
        }


        public async Task<IActionResult> Logout([FromQuery(Name ="ReturnUrl")]  string ReturnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(ReturnUrl);

        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            //1. aşama kullanıcı oluştur.
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,

            };

            //2. aşama kullanıcı kaydet.
            var result = await _userManager.CreateAsync(user,model.Password);

            //3. aşama başarılı ise rol tanımı gerçekleştirilir.
            if (result.Succeeded)
            {
                var roleResult = await _userManager
                    .AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                    return RedirectToAction("Login", new {ReturnUrl="/"});
            }
            else
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View();
        }


        public IActionResult AccessDenied([FromQuery(Name ="ReturnUrl")] string returnUrl)
        {
            return View();
        }
    }
}
