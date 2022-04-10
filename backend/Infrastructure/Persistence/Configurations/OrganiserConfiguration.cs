using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.HasOne(x => x.User)
                .WithMany(x => x.Organisers)
                .HasForeignKey(x => x.UserId);
        }
    }
}
