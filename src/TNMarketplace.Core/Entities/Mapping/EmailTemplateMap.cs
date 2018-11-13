using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class EmailTemplateMap : IEntityTypeConfiguration<EmailTemplate>
    {

        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Body)
                .IsRequired();

            // Table & Column Mappings
            builder.ToTable("EmailTemplates");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Slug).HasColumnName("Slug");
            builder.Property(t => t.Subject).HasColumnName("Subject");
            builder.Property(t => t.Body).HasColumnName("Body");
            builder.Property(t => t.SendCopy).HasColumnName("SendCopy");
        }
    }
}
