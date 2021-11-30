using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ILikeService
    {
        Task<Like> AddLike(LikeDto like);

        Task DeleteLike(int id);

        Task<Like> GetLike(int id);

        Task<DbSet<Like>> GetAllLike();
    }
}
