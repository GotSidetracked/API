using KTU_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Repositories
{
    public interface ICommentRespository
    {
        Task<Comment> GetAsync(int PathId, int CommentID);
        Task<List<Comment>> GetAsync(int PathId);
        Task InsertAsync(Comment CommentInfo);
        Task UpdateAsync(Comment CommentInfo);
        Task DeleteAsync(Comment CommentInfo);
    }
    public class CommentRepository : ICommentRespository
    {
        private readonly DBContextModel dbContextModel;

        public CommentRepository(DBContextModel DBContextModel)
        {
            dbContextModel = DBContextModel;
        }

        public async Task DeleteAsync(Comment CommentInfo)
        {
           dbContextModel.Comments.Remove(CommentInfo);
           await dbContextModel.SaveChangesAsync();
        }

        public async Task<Comment> GetAsync(int PathId, int CommentID)
        {
           return await dbContextModel.Comments.FirstOrDefaultAsync(o => o.Id == CommentID && o.Path.Id == PathId);
        }

        public async Task<List<Comment>> GetAsync(int PathId)
        {
            return await dbContextModel.Comments.Where(o => o.Path.Id == PathId).ToListAsync();
        }

        public async Task InsertAsync(Comment CommentInfo)
        {
            dbContextModel.Comments.Add(CommentInfo);
            await dbContextModel.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment CommentInfo)
        {
            dbContextModel.Comments.Update(CommentInfo);
            await dbContextModel.SaveChangesAsync();
        }
    }
}
