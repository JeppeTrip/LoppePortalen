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
    public class BookingImageConfiguration : IEntityTypeConfiguration<BookingImage>
    {
        public void Configure(EntityTypeBuilder<BookingImage> builder)
        {
            builder.HasKey(x => new { x.BookingId, x.ImageTitle });

            builder.HasOne(x => x.Booking)
                .WithOne(x => x.BannerImage);
        }
    }
}
