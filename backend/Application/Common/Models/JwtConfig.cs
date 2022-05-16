using System;

namespace Application.Common.Models
{
    //TODO: maybe move this.
    public class JwtConfig
    {
        public string Secret { get; set; }
        public TimeSpan ExpiryTimeFrame { get; set; }
    }
}
