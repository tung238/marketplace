using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class MetaFieldMap : IEntityTypeConfiguration<MetaField>
    {

        public void Configure(EntityTypeBuilder<MetaField> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                builder.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(t => t.Placeholder)
                    .HasMaxLength(255);

                // Table & Column Mappings
                builder.ToTable("MetaFields");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.Name).HasColumnName("Name");
                builder.Property(t => t.Placeholder).HasColumnName("Placeholder");
                builder.Property(t => t.ControlTypeID).HasColumnName("ControlTypeID");
                builder.Property(t => t.Options).HasColumnName("Options");
                builder.Property(t => t.Required).HasColumnName("Required");
                builder.Property(t => t.Searchable).HasColumnName("Searchable");
                builder.Property(t => t.Ordering).HasColumnName("Ordering");
            }
        }
    }
}
