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
    public class StallConfiguration : IEntityTypeConfiguration<Stall>
    {
        public void Configure(EntityTypeBuilder<Stall> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.StallType)
                .WithMany(x => x.Stalls);

            builder.HasMany(x => x.Bookings)
                .WithOne(x => x.Stall)
                .HasForeignKey(x => x.StallId);

            builder.HasOne(x => x.MarketInstance)
                .WithMany(x => x.Stalls)
                .HasForeignKey(x => x.MarketInstanceId);
        } 
    }
}
