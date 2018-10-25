using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ContentPageMap : IEntityTypeConfiguration<ContentPage>
    {

        public void Configure(EntityTypeBuilder<ContentPage> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.Description)
                .HasMaxLength(200);

            builder.Property(t => t.Template)
                .HasMaxLength(200);

            builder.Property(t => t.Keywords)
                .HasMaxLength(200);

            builder.Property(t => t.UserID)
                .HasMaxLength(128);

            // Table & Column Mappings
            builder.ToTable("ContentPages");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Slug).HasColumnName("Slug");
            builder.Property(t => t.Title).HasColumnName("Title");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Html).HasColumnName("Html");
            builder.Property(t => t.Template).HasColumnName("Template");
            builder.Property(t => t.Ordering).HasColumnName("Ordering");
            builder.Property(t => t.Keywords).HasColumnName("Keywords");
            builder.Property(t => t.UserID).HasColumnName("UserID");
            builder.Property(t => t.Published).HasColumnName("Published");
            builder.Property(t => t.Created).HasColumnName("Created");
            builder.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
        }
    }
}
