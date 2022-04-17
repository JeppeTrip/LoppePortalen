using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Organiser> Organisers { get; set; }
        DbSet<Address> Address { get; set; }
        DbSet<ContactInfo> ContactInformations { get; set; }
        DbSet<MarketTemplate> MarketTemplates { get; set; }
        DbSet<MarketInstance> MarketInstances { get; set; }
        DbSet<Domain.Entities.User> UserInfo { get; set; }
        DbSet<Stall> Stalls { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<StallType> StallTypes { get; set; }
        DbSet<Merchant> Merchants { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
