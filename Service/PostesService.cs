using FacebookApiTest.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;
using Service.Mapper;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PostesService : IRepository<Poste>
    {
        static PostesService() => MapperConfig.RegisterPosteMapping();

        private readonly IRepository<User> userRepository;
        private readonly IRepository<Poste> postesRepository;

        public PostesService(IRepository<Poste> postesRepository, IRepository<User> userRepository)
        {
            this.postesRepository = postesRepository;
            this.userRepository = userRepository;
        }       
      

        public async Task<DbSet<Poste>> GetAll()
        {

            var post= await postesRepository.GetAll();
            return post;
        }

        public async Task<List<Poste>> GetAllWithComment()
        {

            var post = await postesRepository.GetAll();
            return post
                .Include(x => x.Comments)
                    .ThenInclude(x => x.Likes)
                .Include(t=>t.User)
                .Include(r=>r.Likes)
                .ToList();
        }

        public async Task<Poste> AddAsync(PostDto postes)
        {
            if (postes == null)
            {
                throw new ArgumentException();
            }

            var user = await userRepository.GetAsyncById(postes.UserId);

            if ( user == null)
            {
                throw new KeyNotFoundException("User can't be null");
            }

            var postDao = new Poste()
            {
                Description = postes.Description,
                CreateOn = DateTime.Now,
                User = user
            };
            return await postesRepository.AddAsync(postDao);
        }

        public Task<Poste> UpdateAsync(Poste model)
        {
            throw new NotImplementedException();
        }

        public async Task<Poste> GetAsyncById(int id)
        {
            if (id < 1)
            {
                throw new KeyNotFoundException("id must be bigger than 0");
            }
            return await postesRepository.GetAsyncById(id);
        }

        public async Task DeleteAsync(int id)
        {
            var posteToDelete = await postesRepository.GetAsyncById(id);

            if (posteToDelete == null)
            {
                throw new KeyNotFoundException();
            }

            await postesRepository.DeleteAsync(id);
        }

        public Task<Poste> AddAsync(Poste model)
        {
            throw new NotImplementedException();
        }
    }
}
