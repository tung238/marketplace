using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class CategoryStatMap : IEntityTypeConfiguration<CategoryStat>
    {

        public void Configure(EntityTypeBuilder<CategoryStat> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            builder.ToTable("CategoryStats");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
            builder.Property(t => t.Count).HasColumnName("Count");

            // Relationships
            builder.HasOne(t => t.Category)
                .WithMany(t => t.CategoryStats).IsRequired()
                .HasForeignKey(d => d.CategoryID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
