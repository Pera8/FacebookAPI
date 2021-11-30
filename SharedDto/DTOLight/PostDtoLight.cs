using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDto.DTOLight
{
    public class PostDtoLight
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public LikesDtoLight LikesDtoLight { get; set; }
        public CommentDtoLight CommentDtoLight { get; set; }
    }
}
