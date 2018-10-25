using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ListingMap : IEntityTypeConfiguration<Listing>
    {

        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.UserID)
                .IsRequired();
                //.HasMaxLength(128);

            builder.Property(t => t.Currency)
                .IsFixedLength()
                .HasMaxLength(3);

            builder.Property(t => t.ContactName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.ContactEmail)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.ContactPhone)
                .HasMaxLength(50);

            builder.Property(t => t.IP)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Location)
                .HasMaxLength(250);

            // Table & Column Mappings
            builder.ToTable("Listings");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Title).HasColumnName("Title");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
            builder.Property(t => t.ListingTypeID).HasColumnName("ListingTypeID");
            builder.Property(t => t.UserID).HasColumnName("UserID");
            builder.Property(t => t.Price).HasColumnName("Price");
            builder.Property(t => t.Currency).HasColumnName("Currency");
            builder.Property(t => t.ContactName).HasColumnName("ContactName");
            builder.Property(t => t.ContactEmail).HasColumnName("ContactEmail");
            builder.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            builder.Property(t => t.ShowPhone).HasColumnName("ShowPhone");
            builder.Property(t => t.Active).HasColumnName("Active");
            builder.Property(t => t.Enabled).HasColumnName("Enabled");
            builder.Property(t => t.ShowEmail).HasColumnName("ShowEmail");
            builder.Property(t => t.Premium).HasColumnName("Premium");
            builder.Property(t => t.Expiration).HasColumnName("Expiration");
            builder.Property(t => t.IP).HasColumnName("IP");
            builder.Property(t => t.Location).HasColumnName("Location");
            builder.Property(t => t.Latitude).HasColumnName("Latitude");
            builder.Property(t => t.Longitude).HasColumnName("Longitude");
            builder.Property(t => t.Created).HasColumnName("Created");
            builder.Property(t => t.LastUpdated).HasColumnName("LastUpdated");

            // Relationships
            builder.HasOne(t => t.AspNetUser)
                .WithMany(t => t.Listings).IsRequired()
                .HasForeignKey(d => d.UserID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Category)
                .WithMany(t => t.Listings).IsRequired()
                .HasForeignKey(d => d.CategoryID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.ListingType)
                .WithMany(t => t.Listings).IsRequired()
                .HasForeignKey(d => d.ListingTypeID).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
