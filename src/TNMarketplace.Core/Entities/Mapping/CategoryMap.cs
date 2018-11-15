using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(t => t.IconClass).HasMaxLength(20);
            // Table & Column Mappings
            builder.ToTable("Categories");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Slug).HasColumnName("Slug");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Parent).HasColumnName("Parent");
            builder.Property(t => t.Enabled).HasColumnName("Enabled");
            builder.Property(t => t.Ordering).HasColumnName("Ordering");
            builder.Property(t => t.IconClass).HasColumnName("IconClass");
        }
    }
}
