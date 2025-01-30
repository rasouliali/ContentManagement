namespace CM.UI.Helpers
{
    public class APIMethodUrls
    {
        public static string JsonContent { get; private set; } = "application/json";
        public static string SelectedPost { get; private set; } = "SelectedPost";
        public static string Post { get; private set; } = "Post";
        public static string PostForDashboardAdmin { get; private set; } = "Post/GetPostsForDashboardAdmin";
        public static string Post_AddEditPostByAdmin { get; private set; } = "Post/AddEditPostByAdmin";
        public static string PostComment { get; private set; } = "PostComment";
        public static string SignUp { get; private set; } = "UserManager/Register";
        public static string Login { get; private set; } = "UserManager/Login";
        public static string Logout { get; private set; } = "UserManager/Logout";
    }
}
