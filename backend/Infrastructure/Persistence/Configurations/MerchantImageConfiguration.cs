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
    public class MerchantImageConfiguration : IEntityTypeConfiguration<MerchantImage>
    {
        public void Configure(EntityTypeBuilder<MerchantImage> builder)
        {
            builder.HasKey(x => new { x.MerchantId, x.ImageTitle });

            builder.HasOne(x => x.Merchant)
                .WithOne(x => x.BannerImage);
        }
    }
}
