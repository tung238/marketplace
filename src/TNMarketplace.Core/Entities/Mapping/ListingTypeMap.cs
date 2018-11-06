using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ListingTypeMap : IEntityTypeConfiguration<ListingType>
    {

        public void Configure(EntityTypeBuilder<ListingType> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.ButtonLabel)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(t => t.PriceUnitLabel)
                .HasMaxLength(20);

            builder.Property(t => t.OrderTypeLabel)
                .HasMaxLength(20);

            // Table & Column Mappings
            builder.ToTable("ListingTypes");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.ButtonLabel).HasColumnName("ButtonLabel");
            builder.Property(t => t.PriceUnitLabel).HasColumnName("PriceUnitLabel");
            builder.Property(t => t.OrderTypeID).HasColumnName("OrderTypeID");
            builder.Property(t => t.OrderTypeLabel).HasColumnName("OrderTypeLabel");
            builder.Property(t => t.PaymentEnabled).HasColumnName("PaymentEnabled");
            builder.Property(t => t.PriceEnabled).HasColumnName("PriceEnabled");
            builder.Property(t => t.ShippingEnabled).HasColumnName("ShippingEnabled");
            builder.Property(t => t.Slug).HasColumnName("Slug");
        }
    }
}
