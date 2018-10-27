using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class MessageParticipantMap : IEntityTypeConfiguration<MessageParticipant>
    {

        public void Configure(EntityTypeBuilder<MessageParticipant> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                builder.Property(t => t.UserID)
                    .IsRequired()
                    .HasMaxLength(128);

                // Table & Column Mappings
                builder.ToTable("MessageParticipant");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.MessageThreadID).HasColumnName("MessageThreadID");
                builder.Property(t => t.UserID).HasColumnName("UserID");

                // Relationships
                builder.HasOne(t => t.AspNetUser)
                    .WithMany(t => t.MessageParticipants).IsRequired()
                    .HasForeignKey(d => d.UserID).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(t => t.MessageThread)
                    .WithMany(t => t.MessageParticipants).IsRequired()
                    .HasForeignKey(d => d.MessageThreadID);

            }
        }
    }
}
