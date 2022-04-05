using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Tokens
    {
        public string JwtToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
