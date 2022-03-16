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
    public class OrganiserConfiguration : IEntityTypeConfiguration<Organiser>
    {
        public void Configure(EntityTypeBuilder<Organiser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Address>(x => x.Address);

            builder.HasMany<ContactInfo>(x => x.ContactInfoList)
                .WithOne(x => x.Organiser)
                .IsRequired(false);

            builder.HasMany(x => x.MarketTemplates)
                .WithOne(x => x.Organiser)
                .IsRequired(false);
        }
    }
}
