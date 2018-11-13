using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ListingPictureMap : IEntityTypeConfiguration<ListingPicture>
    {

        public void Configure(EntityTypeBuilder<ListingPicture> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            builder.ToTable("ListingPictures");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.ListingID).HasColumnName("ListingID");
            builder.Property(t => t.Url).HasColumnName("Url");
            builder.Property(t => t.Ordering).HasColumnName("Ordering");

            // Relationships
            builder.HasOne(t => t.Listing)
                .WithMany(t => t.ListingPictures)
                .HasForeignKey(d => d.ListingID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
