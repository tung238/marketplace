using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class RegionMap : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);
            // Properties
            builder.Property(t => t.ID).ValueGeneratedNever();

            // Properties
            builder.Property(t => t.Name).IsRequired()
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Slug).IsRequired()
                .HasMaxLength(200);
            builder.Property(t => t.Type)
               .HasMaxLength(200);
            builder.Property(t => t.NameWithType).IsRequired()
               .HasMaxLength(400);
            builder.Property(t => t.Code).IsRequired()
               .HasMaxLength(10);
            // Table & Column Mappings
            builder.ToTable("Regions");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Slug).HasColumnName("Slug");
            builder.Property(t => t.Type).HasColumnName("Type");
            builder.Property(t => t.NameWithType).HasColumnName("NameWithType");
            builder.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
