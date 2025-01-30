using CleanArchitectrure.Application.Interface.Persistence;
using CM.Domain.Entities.PostEntities;
using System.Threading.Tasks;

namespace CM.Application.Interfaces
{
    public interface IPostCommentRepository:IGenericRepository<PostComment>
    {
        Task<List<PostComment>> GetByPostIdAsync(int postId);
    }
}