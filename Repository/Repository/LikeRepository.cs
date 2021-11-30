using FacebookApiTest.Repository;
using Repository.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class LikeRepository: ILikeRepository
    {
        private readonly AppDbContext dbContext;
        public LikeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Like> AddAsync(Like like)
        {
            if (like == null)
            {
                throw new ArgumentNullException($"Like entity must not be null");
            }

            await dbContext.AddAsync(like);
            await dbContext.SaveChangesAsync();
            return like;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await dbContext.Likes.FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                dbContext.Likes.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteLike(int id)
        {
            var result = await dbContext.Likes.FirstOrDefaultAsync(e => e.Id == id );

            if (result != null)
            {
                dbContext.Likes.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<DbSet<Like>> GetAll()
        {
            return dbContext.Set<Like>();
        }

        public async Task<Like> GetAsyncById(int id)
        {
            var result = dbContext.Set<Like>().SingleOrDefault(e => e.Id == id);
            return result;
        }

        public async Task<Like> GetLike(int id)
        {
            
            var result = dbContext.Set<Like>().SingleOrDefault(e => e.Id == id );
            return result;
        }

        public Task<Like> UpdateAsync(Like model)
        {
            throw new NotImplementedException();
        }
    }
}
