using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Common
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<RefreshToken> RefreshTokens { get; set; }
    }
}
