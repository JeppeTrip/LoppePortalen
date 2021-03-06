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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.IdentityId);

            builder.HasMany<Organiser>(x => x.Organisers)
                .WithOne(x => x.User);

            builder.HasMany(x => x.Merchants)
                .WithOne(x => x.User);
        }
    }
}
