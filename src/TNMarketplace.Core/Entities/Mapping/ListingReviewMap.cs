using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ListingReviewMap : IEntityTypeConfiguration<ListingReview>
    {   

        public void Configure(EntityTypeBuilder<ListingReview> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Title)
                .HasMaxLength(250);

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.UserFrom)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(t => t.UserTo)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            builder.ToTable("ListingReviews");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Title).HasColumnName("Title");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Rating).HasColumnName("Rating");
            builder.Property(t => t.ListingID).HasColumnName("ListingID");
            builder.Property(t => t.OrderID).HasColumnName("OrderID");
            builder.Property(t => t.UserFrom).HasColumnName("UserFrom");
            builder.Property(t => t.UserTo).HasColumnName("UserTo");
            builder.Property(t => t.Active).HasColumnName("Active");
            builder.Property(t => t.Enabled).HasColumnName("Enabled");
            builder.Property(t => t.Spam).HasColumnName("Spam");
            builder.Property(t => t.Created).HasColumnName("Created");

            // Relationships
            builder.HasOne(t => t.AspNetUserFrom)
                .WithMany(t => t.ListingReviewsUserFrom).IsRequired()
                .HasForeignKey(d => d.UserFrom).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.AspNetUserTo)
                .WithMany(t => t.ListingReviewsUserTo).IsRequired()
                .HasForeignKey(d => d.UserTo).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Listing)
                .WithMany(t => t.ListingReviews)
                .HasForeignKey(d => d.ListingID).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Order)
                .WithMany(t => t.ListingReviews)
                .HasForeignKey(d => d.OrderID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
