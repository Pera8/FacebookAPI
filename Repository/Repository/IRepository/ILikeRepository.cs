using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task DeleteLike(int id);
        Task<Like> GetLike(int id);

    }
}
