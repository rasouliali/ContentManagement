using CM.UI.Helpers;
using CM.UI.Models;
using CM.UI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountRepository _repository;

        public AccountController(ILogger<AccountController> logger, IConfiguration config, AccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpDto input)
        {
            if (ModelState.IsValid)
            {
                var res = await _repository.SignUpAsync(input);
                ViewBag.IsSuccess = res;
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInDto input)
        {
            var res = await _repository.LoginAsync(input);
            if (res.Item1 != null)
            {
                await AuthData.SetSignedInAsync(res.Item1, HttpContext, input.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsSuccess = res.Item2;
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            var dto = AuthData.GetUserData(HttpContext);
            var res = await _repository.LogoutAsync(dto.Token, dto.UserId);
            if (res)
            {
                await AuthData.DeleteLogoutAsync(HttpContext);
            }
            ViewBag.IsSuccess = res;
            return RedirectToAction("Login", "Account");
        }


    }
}
