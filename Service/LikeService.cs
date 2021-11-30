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
    public class LikeService : ILikeService
    {
        static LikeService() => MapperConfig.RegisterLikeMapping();

        private readonly ILikeRepository _likeRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Poste> _postesRepository;

        public LikeService(ILikeRepository likeRepository, IRepository<Comment> commentRepository, IRepository<User> userRepository, IRepository<Poste> postesRepository)
        {
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postesRepository = postesRepository;
        }
        public async Task<Like> AddLike(LikeDto like)
        {
            if (like == null)
            {
                throw new ArgumentException();
            }

            var post = await _postesRepository.GetAsyncById(like.PosteId);
            var user = await _userRepository.GetAsyncById(like.UserId);
            var comment = await _commentRepository.GetAsyncById(like.CommentId);

            if ( user == null )
            {
                throw new KeyNotFoundException("User are missing");
            }
            else if(post==null && comment == null)
            {
                throw new KeyNotFoundException("Post or Comment are missing");
            }

            var _like = new Like()
            {
                User = user
            };
            if (like.CommentId > 0)
            {
                _like.Comment = comment;
            }
            else if (like.PosteId > 0)
            {
                _like.Poste = post;
            }
            return await _likeRepository.AddAsync(_like);
        }

        public async Task DeleteLike(int id)
        {
            if (id < 0)
            {
                throw new KeyNotFoundException("id must be bigger than 0");
            }
            await _likeRepository.DeleteLike(id);
        }

        public async Task<DbSet<Like>> GetAllLike()
        {
            return await _likeRepository.GetAll();
        }

        public async Task<Like> GetLike(int id)
        {
            var result = await _likeRepository.GetLike(id);

            if (result == null)
            {
                throw new ArgumentException();
            }
            return result;
        }
    }
}
