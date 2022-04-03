using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class AuthResult : Result
    {
        public string Token { get; set; } = string.Empty;
        public AuthResult(bool succeeded, IEnumerable<string> errors, string? token) : base(succeeded, errors)
        {
            if (token != null)
            {
                Token = token;
            }
        }
    }
}
