using Microsoft.AspNetCore.Identity;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookApiTest.Repository.Models
{
    public class User: IdentityUser<int>, IBaseModel
    {
        public List<Poste> Postes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Like> Likes { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(150)]
        public string City { get; set; }
        public bool IsDeleted { get; set; }
    }
}
