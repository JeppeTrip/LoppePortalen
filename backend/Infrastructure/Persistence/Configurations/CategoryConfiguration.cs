using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .HasConversion<string>()
                .IsRequired();

            builder.HasKey(x => x.Name);

            builder.HasData(
                Enum.GetValues(typeof(ItemCategory))
                .Cast<ItemCategory>()
                .Select(x => new Category()
                {
                    Name = x
                }));
        }
    }
}
