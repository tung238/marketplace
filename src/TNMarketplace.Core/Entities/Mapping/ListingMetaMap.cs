using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ListingMetaMap : IEntityTypeConfiguration<ListingMeta>
    {

        public void Configure(EntityTypeBuilder<ListingMeta> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Value)
                .IsRequired();

            // Table & Column Mappings
            builder.ToTable("ListingMeta");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.ListingID).HasColumnName("ListingID");
            builder.Property(t => t.FieldID).HasColumnName("FieldID");
            builder.Property(t => t.Value).HasColumnName("Value");

            // Relationships
            builder.HasOne(t => t.Listing)
                .WithMany(t => t.ListingMetas).IsRequired()
                .HasForeignKey(d => d.ListingID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.MetaField)
                .WithMany(t => t.ListingMetas).IsRequired()
                .HasForeignKey(d => d.FieldID).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
