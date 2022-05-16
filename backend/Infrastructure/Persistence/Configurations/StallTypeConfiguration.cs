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
    public class StallTypeConfiguration : IEntityTypeConfiguration<StallType>
    {
        public void Configure(EntityTypeBuilder<StallType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.MarketTemplate)
                .WithMany(x => x.StallTypes);

            builder.HasMany(x => x.Stalls)
                .WithOne(x => x.StallType);
        }
    }
}
