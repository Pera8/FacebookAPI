using FacebookApiTest.Repository.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDto.DTOLight
{
    public class CommentDtoLight
    {
        public int Id { get; set; }
        public LikesDtoLight LikesDtoLight { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
    }
}
