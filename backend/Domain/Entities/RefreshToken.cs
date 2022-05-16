using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// This table is to store the refresh tokens, the question is if I can just used the existing AspNetUserToken table.
    /// UserId: Id when logged in.
    /// Token: Refresh token
    /// JwtId: Id Generated what jwt id has been requested.
    /// IsUsed: make sure token is only used once.
    /// IsRewoked: Make sure tokens are valid.
    /// </summary>
    public class RefreshToken : AuditableEntity
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRewoked { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
