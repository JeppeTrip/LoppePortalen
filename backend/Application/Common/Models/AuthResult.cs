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
        public string RefreshToken { get; set; } = string.Empty ;

        public AuthResult(bool succeeded, IEnumerable<string> errors, string? token, string? refreshToken) : base(succeeded, errors)
        {
            if (token != null)
            {
                Token = token;
            }
            if (refreshToken != null)
            {
                RefreshToken = refreshToken;
            }
        }

        public AuthResult(Result result, string? token, string? refreshToken) : base(result.Succeeded, result.Errors)
        {
            if (token != null)
            {
                Token = token;
            }
            if (refreshToken != null)
            {
                RefreshToken = refreshToken;
            }
        }
    }
}
