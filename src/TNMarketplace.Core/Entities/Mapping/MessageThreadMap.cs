using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class MessageThreadMap : IEntityTypeConfiguration<MessageThread>
    {

        public void Configure(EntityTypeBuilder<MessageThread> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                builder.Property(t => t.Subject)
                    .HasMaxLength(200);

                // Table & Column Mappings
                builder.ToTable("MessageThread");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.Subject).HasColumnName("Subject");
                builder.Property(t => t.ListingID).HasColumnName("ListingID");

                // Relationships
                builder.HasOne(t => t.Listing)
                    .WithMany(t => t.MessageThreads)
                    .HasForeignKey(d => d.ListingID);

            }
        }
    }
}
