using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class LikeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PosteId { get; set; }
        public int CommentId { get; set; }
    }
}
