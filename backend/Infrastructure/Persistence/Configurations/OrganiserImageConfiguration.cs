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
    public class OrganiserImageConfiguration : IEntityTypeConfiguration<OrganiserImage>
    {
        public void Configure(EntityTypeBuilder<OrganiserImage> builder)
        {
            builder.HasKey(x => new { x.OrganiserId, x.ImageTitle });

            builder.HasOne(x => x.Organiser)
                .WithOne(x => x.BannerImage);
        }
    }
}
