using FacebookApiTest.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class UserRole : IdentityUserRole<int>, IDeleteModel
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DataCreated { get; set; }
        public DateTime? DataModified { get; set; }
    }
}
