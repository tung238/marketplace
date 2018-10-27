using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Currency)
                    .IsFixedLength()
                    .HasMaxLength(3);

            builder.Property(t => t.UserProvider)
                    .IsRequired()
                    .HasMaxLength(128);

            builder.Property(t => t.UserReceiver)
                    .IsRequired()
                    .HasMaxLength(128);

            builder.Property(t => t.PaymentPlugin)
                    .HasMaxLength(250);

            // Table & Column Mappings
            builder.ToTable("Orders");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.FromDate).HasColumnName("FromDate");
            builder.Property(t => t.ToDate).HasColumnName("ToDate");
            builder.Property(t => t.ListingID).HasColumnName("ListingID");
            builder.Property(t => t.ListingTypeID).HasColumnName("ListingTypeID");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.Quantity).HasColumnName("Quantity");
            builder.Property(t => t.Price).HasColumnName("Price");
            builder.Property(t => t.Currency).HasColumnName("Currency");
            builder.Property(t => t.ApplicationFee).HasColumnName("ApplicationFee");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Message).HasColumnName("Message");
            builder.Property(t => t.UserProvider).HasColumnName("UserProvider");
            builder.Property(t => t.UserReceiver).HasColumnName("UserReceiver");
            builder.Property(t => t.PaymentPlugin).HasColumnName("PaymentPlugin");
            builder.Property(t => t.Created).HasColumnName("Created");
            builder.Property(t => t.Modified).HasColumnName("Modified");

            // Relationships
            builder.HasOne(t => t.AspNetUserProvider)
                    .WithMany(t => t.OrdersProvider).IsRequired()
                    .HasForeignKey(d => d.UserProvider).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.AspNetUserReceiver)
                    .WithMany(t => t.OrdersReceiver).IsRequired()
                    .HasForeignKey(d => d.UserReceiver).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Listing)
                    .WithMany(t => t.Orders).IsRequired()
                    .HasForeignKey(d => d.ListingID);

        }
    }
}
