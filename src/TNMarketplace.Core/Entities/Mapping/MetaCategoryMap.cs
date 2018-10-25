using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class MetaCategoryMap : IEntityTypeConfiguration<MetaCategory>
    {

        public void Configure(EntityTypeBuilder<MetaCategory> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                // Table & Column Mappings
                builder.ToTable("MetaCategories");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
                builder.Property(t => t.FieldID).HasColumnName("FieldID");

                // Relationships
                builder.HasOne(t => t.Category)
                    .WithMany(t => t.MetaCategories).IsRequired()
                    .HasForeignKey(d => d.CategoryID).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(t => t.MetaField)
                    .WithMany(t => t.MetaCategories).IsRequired()
                    .HasForeignKey(d => d.FieldID).OnDelete(DeleteBehavior.Cascade);

            }
        }
    }
}
