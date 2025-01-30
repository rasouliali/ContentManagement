using CleanArchitectrure.Application.Dto;
using CM.Application.Dto;
using CM.UI.Helpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CM.UI.Repositories
{
    public class PostRepository
    {
        private readonly ApiCaller _apiCaller;
        private readonly string _apiUrl;
        private readonly ILogger<PostRepository> _logger;
        public PostRepository(ILogger<PostRepository> logger, ApiCaller apiCaller, IConfiguration config)
        {
            this._apiCaller = apiCaller;
            _apiUrl = config.GetValue<string>("ApiUrl");
            _logger = logger;
        }

        internal async Task<string> AddCommentAsync(PostCommentDto input, UserLoginDto dt)
        {
            var res = await _apiCaller.CallPostApiAsync(_apiUrl + APIMethodUrls.PostComment, APIMethodUrls.JsonContent, JsonConvert.SerializeObject(input), dt.Token, dt.UserId);
            return (res ?? "").Contains("(401) Unauthorized") ? "401" : "";
        }

        internal async Task<string> AddPostAsync(PostDto input, IFormFile fileData, UserLoginDto dt)
        {
            input.ImageFileName= FileHelper.GetFile(fileData);

            var url = _apiUrl + APIMethodUrls.Post;
            if (dt.Roles == "Admin")
                url = _apiUrl + APIMethodUrls.Post_AddEditPostByAdmin;

            var res =await _apiCaller.CallPostApiAsync(url, APIMethodUrls.JsonContent, JsonConvert.SerializeObject(input), dt.Token, dt.UserId);


            return (res??"").Contains("(401) Unauthorized")? "401":"";
        }

        internal async Task<string> AddSelectedPost(SelectedPostDto input, UserLoginDto dt)
        {
            var inputJson = JsonConvert.SerializeObject(input);
            var res = await _apiCaller.CallPostApiAsync(_apiUrl + Helpers.APIMethodUrls.SelectedPost,APIMethodUrls.JsonContent, inputJson, dt.Token, dt.UserId);
            if (res.StartsWith("errorOccured:") == false)
            {
                var obj = JObject.Parse(res);
                if (obj.Value<bool>("success"))
                {
                    var data = obj.Value<int>("data");
                    return data+"";
                }
                return (res ?? "").Contains("(401) Unauthorized") ? "401" : "";
            }
            return null;
        }

        internal async Task<bool> DelPostsForAdminAsync(int postid,UserLoginDto dt)
        {
            var res = await _apiCaller.CallDeleteApiAsync(_apiUrl + Helpers.APIMethodUrls.Post, Helpers.APIMethodUrls.JsonContent,@"{""Id"":"+postid+"}", dt.Token, dt.UserId);
            if (res.StartsWith("errorOccured:") == false)
            {
                var obj = JObject.Parse(res);
                if (obj.Value<bool>("success"))
                {
                    return obj.Value<bool>("data");
                    //var data = JsonConvert.DeserializeObject<List<PostDto>>(obj.GetValue("data").ToString());
                    //return data;
                }
            }
            return false;
        }

        internal async Task<List<PostDto>> GetPostListAsync()
        {
            var res = await _apiCaller.CallGetApiAsync(_apiUrl + Helpers.APIMethodUrls.Post, "", 0);
            if (res.StartsWith("errorOccured:") == false)
            {
                var obj = JObject.Parse(res);
                if (obj.Value<bool>("success"))
                {
                    var data = JsonConvert.DeserializeObject<List<PostDto>>(obj.GetValue("data").ToString());
                    return data;
                }
            }
            return null;
        }
        
        internal async Task<List<PostDto>> GetPostsForDashboardAdminAsync(UserLoginDto dt)
        {
            var res = await _apiCaller.CallGetApiAsync(_apiUrl + Helpers.APIMethodUrls.PostForDashboardAdmin, dt.Token, dt.UserId);
            if (res.StartsWith("errorOccured:") == false)
            {
                var obj = JObject.Parse(res);
                if (obj.Value<bool>("success"))
                {
                    var data = JsonConvert.DeserializeObject<List<PostDto>>(obj.GetValue("data").ToString());
                    return data;
                }
            }
            return null;
        }

        internal async Task<List<SelectedPostDto>> GetSelectedPost(UserLoginDto dt)
        {
            var res = await _apiCaller.CallGetApiAsync(_apiUrl + Helpers.APIMethodUrls.SelectedPost, dt.Token,dt.UserId);
            if (res.StartsWith("errorOccured:") == false)
            {
                var obj = JObject.Parse(res);
                if (obj.Value<bool>("success"))
                {
                    var data = JsonConvert.DeserializeObject<List<SelectedPostDto>>(obj.GetValue("data").ToString());
                    return data;
                }
            }
            return null;
        }
    }
}
