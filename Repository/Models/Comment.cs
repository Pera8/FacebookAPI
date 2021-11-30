using FacebookApiTest.Repository.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Comment : Base
    {
        [Required]
        public string Text { get; set; }
        public User User { get; set; }
        public Poste Poste { get; set; }
        public List<Like> Likes { get; set; }

    }
}
