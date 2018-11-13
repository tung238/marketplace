using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ListingStatMap : IEntityTypeConfiguration<ListingStat>
    {

        public void Configure(EntityTypeBuilder<ListingStat> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            builder.ToTable("ListingStats");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.CountView).HasColumnName("CountView");
            builder.Property(t => t.CountSpam).HasColumnName("CountSpam");
            builder.Property(t => t.CountRepeated).HasColumnName("CountRepeated");
            builder.Property(t => t.ListingID).HasColumnName("ListingID");

            // Relationships
            builder.HasOne(t => t.Listing)
                .WithMany(t => t.ListingStats).IsRequired()
                .HasForeignKey(d => d.ListingID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
