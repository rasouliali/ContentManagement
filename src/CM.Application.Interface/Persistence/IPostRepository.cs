using CleanArchitectrure.Application.Interface.Persistence;
using CM.Domain.Entities.PostEntities;

namespace CM.Application.Interfaces
{
    public interface IPostRepository: IGenericRepository<Post>
    {
        Task<List<Post>> GetAllForAdminDashboardAsync(int UserId);
        //Task<List<Post>> GetAll();
        //Task<int> AddEditPost(Post input);
        //Task<bool> DeletePost(int Id);

    }
}