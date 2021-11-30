using FacebookApiTest.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;
using Service.Mapper;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class CommentsService : IRepository<Comment>
    {
        static CommentsService() => MapperConfig.RegisterCommentMapping();

        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Poste> _postesRepository;

        public CommentsService(IRepository<Comment> commentRepository, IRepository<User> userRepository, IRepository<Poste> postesRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postesRepository = postesRepository;
        }
       

        public Task<Comment> UpdateAsync(Comment model)
        {
            throw new NotImplementedException();
        }

        public async Task<DbSet<Comment>> GetAll()
        {
            var comments = await _commentRepository.GetAll();
            // return comments.Include(x => x.Likes).ToList();
            return comments;
        }

        public async Task<Comment> AddAsync(CommentDto comment)
        {
            if (comment == null)
            {
                throw new ArgumentException();
            }

            var post = await _postesRepository.GetAsyncById(comment.PosteId);
            var user = await _userRepository.GetAsyncById(comment.UserId);

            if(post==null || user == null)
            {
                throw new KeyNotFoundException(" Poste or User can't find ");
            }

            var commentDao = new Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                CreateOn = DateTime.Now,
                User = user,
                Poste = post,
            };

            return await _commentRepository.AddAsync(commentDao);
        }

        public async Task<Comment> GetAsyncById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("id must be bigger than 0"); 
            }
            var result = await _commentRepository.GetAsyncById(id);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var commentToDelete = await _commentRepository.GetAsyncById(id);
            if (commentToDelete == null)
            {
                throw new KeyNotFoundException("No comment ");
            }
            await _commentRepository.DeleteAsync(id);
        }

        public Task<Comment> AddAsync(Comment model)
        {
            throw new NotImplementedException();
        }
    }
}
