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
        DbSet<Booking> Bookings { get; set; }
        DbSet<Category> ItemCategories { get; set; }
        DbSet<MerchantContactInfo> MerchantContactInfos { get; set;}
        DbSet<OrganiserImage> OrganiserImages { get; set; }
        DbSet<BookingImage> BookingImages{ get; set; }
        DbSet<MerchantImage> MerchantImages { get; set; }
        DbSet<MarketImage> MarketImages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
