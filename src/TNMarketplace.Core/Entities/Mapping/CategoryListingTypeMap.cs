using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class CategoryListingTypeMap : IEntityTypeConfiguration<CategoryListingType>
    {

        public void Configure(EntityTypeBuilder<CategoryListingType> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            builder.ToTable("CategoryListingTypes");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
            builder.Property(t => t.ListingTypeID).HasColumnName("ListingTypeID");

            // Relationships
            builder.HasOne(t => t.Category)
                .WithMany(t => t.CategoryListingTypes).IsRequired()
                .HasForeignKey(d => d.CategoryID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.ListingType)
                .WithMany(t => t.CategoryListingTypes).IsRequired()
                .HasForeignKey(d => d.ListingTypeID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
