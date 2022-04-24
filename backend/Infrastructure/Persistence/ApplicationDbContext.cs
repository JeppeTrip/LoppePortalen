using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService): base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
        }


        private void PrepareEntitiesForSave()
        {
            //TODO: Add data time, and user tracking of modifications made to the data.
            var entities = ChangeTracker.Entries<AuditableEntity>();
            foreach (var entry in entities)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        break;
                    case EntityState.Modified:
                        break;
                }
            }
        }

        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ContactInfo> ContactInformations { get; set; }
        public DbSet<MarketTemplate> MarketTemplates { get; set; }
        public DbSet<MarketInstance> MarketInstances { get; set; }
        public DbSet<User> UserInfo { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Stall> Stalls { get; set; }
        public DbSet<StallType> StallTypes { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> ItemCategories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) 
        {
            PrepareEntitiesForSave();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
