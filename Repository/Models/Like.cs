using FacebookApiTest.Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Like:Base
    {
        public User User { get; set; }
        public Poste Poste { get; set; }
        public Comment Comment { get; set; }
    }
}
