namespace CM.API.Controllers
{
    internal static class SharedLogic
    {

        public static int GetUserId(HttpRequest request)
        {
            try
            {
                return int.Parse(request.Headers["UserId"].ToString());
            }
            catch
            {
                return 0;
            }
        }
    }
}