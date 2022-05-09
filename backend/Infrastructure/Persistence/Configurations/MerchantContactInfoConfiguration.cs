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
    public class MerchantContactInfoConfiguration : IEntityTypeConfiguration<MerchantContactInfo>
    {
        public void Configure(EntityTypeBuilder<MerchantContactInfo> builder)
        {
            builder.HasKey(x => new { x.MerchantId, x.Value });

            builder.HasOne(x => x.Merchant)
                .WithMany(x => x.ContactInfo);
        }
    }
}
