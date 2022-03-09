using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //TODO: Add current user and datetimeoffsets here
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
