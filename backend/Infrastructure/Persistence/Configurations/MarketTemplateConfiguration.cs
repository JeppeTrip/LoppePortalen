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
    public class MarketTemplateConfiguration : IEntityTypeConfiguration<MarketTemplate>
    {
        public void Configure(EntityTypeBuilder<MarketTemplate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Organiser)
                .WithMany(x => x.MarketTemplates)
                .IsRequired(true);

            builder.HasMany(x => x.MarketInstances)
                .WithOne(x => x.MarketTemplate)
                .IsRequired(true);
        }
    }
}
