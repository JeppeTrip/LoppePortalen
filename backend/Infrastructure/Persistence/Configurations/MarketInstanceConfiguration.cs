using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
