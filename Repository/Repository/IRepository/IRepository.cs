using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepository<TModel> where TModel: class, IBaseModel
    {
        Task<DbSet<TModel>> GetAll();
        Task<TModel> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task<TModel> GetAsyncById(int id);

        Task DeleteAsync(int id);
    }
}
