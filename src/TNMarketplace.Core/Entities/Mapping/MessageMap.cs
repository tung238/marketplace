using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {

        public void Configure(EntityTypeBuilder<Message> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                builder.Property(t => t.Body)
                    .IsRequired();

                builder.Property(t => t.UserFrom)
                    .IsRequired();
                    //.HasMaxLength(128);

                // Table & Column Mappings
                builder.ToTable("Message");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.MessageThreadID).HasColumnName("MessageThreadID");
                builder.Property(t => t.Body).HasColumnName("Body");
                builder.Property(t => t.UserFrom).HasColumnName("UserFrom");
                builder.Property(t => t.Created).HasColumnName("Created");
                builder.Property(t => t.LastUpdated).HasColumnName("LastUpdated");

                // Relationships
                builder.HasOne(t => t.AspNetUser)
                    .WithMany(t => t.Messages).IsRequired()
                    .HasForeignKey(d => d.UserFrom).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(t => t.MessageThread)
                    .WithMany(t => t.Messages).IsRequired()
                    .HasForeignKey(d => d.MessageThreadID);

            }
        }
    }
}
