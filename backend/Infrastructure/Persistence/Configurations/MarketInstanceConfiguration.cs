using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MarketInstanceConfiguration : IEntityTypeConfiguration<MarketInstance>
    {
        public void Configure(EntityTypeBuilder<MarketInstance> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
