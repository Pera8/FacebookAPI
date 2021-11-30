using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class AuthDto
    {
        public UserAuth UserAuth { get; set; }
        public string Token { get; set; }
    }
}
