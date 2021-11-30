using FacebookApiTest.Repository.Models;
using System.Collections.Generic;

namespace Repository.Models
{
    public class Poste :Base
    {
        public string Description { get; set; }
        public User User { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
