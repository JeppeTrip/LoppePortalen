using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.RefreshJwt
{
    public class RefreshJwtRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
