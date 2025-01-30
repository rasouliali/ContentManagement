using CleanArchitectrure.Application.Interface.Persistence;
using CM.Domain.Entities.PostEntities;

namespace CM.Application.Interfaces
{
    public interface ISelectedPostRepository: IGenericRepository<SelectedPost>
    {
        Task<List<SelectedPost>> GetAllByCurrentUserIdAsync(int userId);

        //Task<List<Post>> GetAll();
        //Task<int> AddEditPost(Post input);
        //Task<bool> DeletePost(int Id);

    }
}