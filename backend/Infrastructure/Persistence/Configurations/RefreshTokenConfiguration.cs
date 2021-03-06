using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            //TODO: Is this actually a valid primary key?
            builder.HasKey(x => x.Token);

            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired(true);
        }
    }
}
