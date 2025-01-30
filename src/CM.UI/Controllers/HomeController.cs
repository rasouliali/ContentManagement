using CM.Application.Dto;
using CM.UI.Helpers;
using CM.UI.Models;
using CM.UI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace CM.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiCaller _apiCaller;
        private readonly string _apiUrl;
        private readonly PostRepository _repository;

        public HomeController(ILogger<HomeController> logger, ApiCaller apiCaller, IConfiguration config, PostRepository postRepository)
        {
            _logger = logger;
            this._apiCaller = apiCaller;
            _apiUrl = config.GetValue<string>("ApiUrl");
            _repository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _repository.GetPostListAsync();
            if (User.Identity.IsAuthenticated)
            {
                var dt = AuthData.GetUserData(HttpContext);
                var res2 = await _repository.GetSelectedPost(dt);
                foreach (var item in res)
                {
                    if (res2 != null && res2.Any(r => item.Id == r.PostId))
                        item.IsSelected = true;
                }
            }
            return View(res);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Dashboard()
        {
            var dt = AuthData.GetUserData(HttpContext);
            var res = await _repository.GetPostsForDashboardAdminAsync(dt);
            return View(res);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var dt = AuthData.GetUserData(HttpContext);
            var res = await _repository.DelPostsForAdminAsync(id,dt);
            return RedirectToAction("Dashboard", "Home");
        }
        [Authorize]
        public IActionResult AddPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SelectedPost(SelectedPostDto input)
        {
            var dt = AuthData.GetUserData(HttpContext);
            var res = await _repository.AddSelectedPost(input,  dt);
            if (res == "401")
                return RedirectToAction("Logout", "Account");
            return RedirectToAction("Index","Home");
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment( PostCommentDto input)
        {
            var dt = AuthData.GetUserData(HttpContext);
            var res = await _repository.AddCommentAsync(input,  dt);
            if (res == "401")
                return RedirectToAction("Logout", "Account");
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPostAsync(PostDto input,IFormFile FileData)
        {
            ModelState["ImageFileName"].ValidationState = ModelValidationState.Valid;
            ModelState["PostComments"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                var dt = AuthData.GetUserData(HttpContext);
                var res = await _repository.AddPostAsync(input, FileData, dt);
                if (res == "401")
                    return RedirectToAction("Logout", "Account");
                else
                    ViewBag.IsSuccess = true;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
