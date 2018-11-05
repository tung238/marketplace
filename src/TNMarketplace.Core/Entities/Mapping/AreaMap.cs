using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class AreaMap : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
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
            builder.Property(t => t.Path).IsRequired();
            builder.Property(t => t.PathWithType).IsRequired();
            // Table & Column Mappings
            builder.ToTable("Areas");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Slug).HasColumnName("Slug");
            builder.Property(t => t.Type).HasColumnName("Type");
            builder.Property(t => t.NameWithType).HasColumnName("NameWithType");
            builder.Property(t => t.Path).HasColumnName("Path");
            builder.Property(t => t.PathWithType).HasColumnName("PathWithType");

            // Relationships
            builder.HasOne(t => t.Region)
                .WithMany(t => t.Areas).IsRequired()
                .HasForeignKey(d => d.RegionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
