using CleanArchitectrure.Application.Interface.Persistence;
using CM.Application.Interfaces;
using CM.Domain.Entities.PostEntities;
using CM.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Infrastructure.Repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        private CMContext db;
        public PostCommentRepository(CMContext db)
        {
            this.db = db;
        }
        public async Task<List<PostComment>> GetByPostIdAsync(int postId)
        {
            var datas = await db.PostComments.Where(r => r.IsDeleted == false && r.PostId == postId).ToListAsync();
            return datas;
        }
        public async Task<PostComment> GetAsync(int id)
        {
            var item = await db.PostComments.FirstOrDefaultAsync(r => r.IsDeleted == false && r.Id == id);
            return item;
        }
        public async Task<int> AddEditAsync(PostComment input)
        {
            if (input.Id == 0)
            {
                db.PostComments.Add(input);
            }
            else
            {
                var before = db.PostComments.Find(input.Id);//update must not be change createuserid and createddate
                if (before != null)
                {
                    input.CreatedDate = before.CreatedDate;
                    input.CreatedUserId = before.CreatedUserId;
                    db.PostComments.Update(input);
                }
                else
                    return 0;
            }
            await db.SaveChangesAsync();
            return input.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostComment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
