using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser 
            { UserName = registerViewModel.Username, 
                Email = registerViewModel.Email };
            var identityResult = await _userManager
                .CreateAsync(identityUser, registerViewModel.Password);

            if(identityResult.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(identityUser, "User");

                if(result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpGet]
        public  IActionResult Login(string returnUrl)
        {
            var model = new LoginRequest
            {
                ReturnUrl = returnUrl,
            };

            return View(model);
        }
    


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginRequest.Username,
                loginRequest.Password, false, false);

            if(signInResult != null && signInResult.Succeeded)
            {

                if(!string.IsNullOrWhiteSpace(loginRequest.ReturnUrl))
                {
                    return RedirectToPage(loginRequest.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }



    }
}
