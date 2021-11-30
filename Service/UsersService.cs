using FacebookApiTest.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Threading.Tasks;

namespace Service
{
    public class UsersService : IRepository<User>
    {
        private readonly IRepository<User> _userRepository;
        public UsersService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> AddAsync(User model)
        {
            throw new NotImplementedException();
        }

        

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DbSet<User>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users;
        }       

        public async Task<User> GetAsyncById(int id)
        {
            var result = await _userRepository.GetAsyncById(id);
            return result;
        }
        

        public Task<User> UpdateAsync(User model)
        {
            throw new NotImplementedException();
        }
    }
}
