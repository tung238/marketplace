using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class SettingDictionaryMap : IEntityTypeConfiguration<SettingDictionary>
    {
        public void Configure(EntityTypeBuilder<SettingDictionary> builder)
        {
            // Primary Key
            builder.HasKey(t => t.ID);

            // Properties
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Value)
                .HasMaxLength(200);

            // Table & Column Mappings
            builder.ToTable("SettingDictionary");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Value).HasColumnName("Value");
            builder.Property(t => t.SettingID).HasColumnName("SettingID");

            // Relationships
            builder.HasOne(t => t.Setting)
                .WithMany(t => t.SettingDictionaries).IsRequired()
                .HasForeignKey(d => d.SettingID);

        }
    }
}
