using FacebookApiTest.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<TModel> : IRepository<TModel>
        where TModel : class, IBaseModel
    {

        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> AddAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await dbContext.AddAsync(model);
                await dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(model)} could not be saved: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var result=await dbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.IsDeleted = true;
                //dbContext.Postes.Remove(result);
                await dbContext.SaveChangesAsync();
            }

           
        }

        public async Task<DbSet<TModel>> GetAll()
        {
            return dbContext.Set<TModel>();
        }

        public async Task<TModel> GetAsyncById(int id)
        {
            return await dbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                dbContext.Update(model);
                await dbContext.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(model)} could not be updated: {ex.Message}");
            }
        }

        
    }
}
