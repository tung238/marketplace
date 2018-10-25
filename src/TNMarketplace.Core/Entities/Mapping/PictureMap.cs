using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class PictureMap : IEntityTypeConfiguration<Picture>
    {

        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.MimeType)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(t => t.SeoFilename)
                .HasMaxLength(200);

            // Table & Column Mappings
            builder.ToTable("Pictures");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.MimeType).HasColumnName("MimeType");
            builder.Property(t => t.SeoFilename).HasColumnName("SeoFilename");
        }
    }
}
