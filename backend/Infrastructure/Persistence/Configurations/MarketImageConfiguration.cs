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
    public class MarketImageConfiguration : IEntityTypeConfiguration<MarketImage>
    {
        public void Configure(EntityTypeBuilder<MarketImage> builder)
        {
            builder.HasKey(x => new { x.MarketTemplateId, x.ImageTitle });

            builder.HasOne(x => x.MarketTemplate)
                .WithOne(x => x.BannerImage);
        }
    }
}
