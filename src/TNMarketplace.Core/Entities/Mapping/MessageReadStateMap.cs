using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class MessageReadStateMap : IEntityTypeConfiguration<MessageReadState>
    {
        public void Configure(EntityTypeBuilder<MessageReadState> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                builder.Property(t => t.UserID)
                    .IsRequired()
                    .HasMaxLength(128);

                // Table & Column Mappings
                builder.ToTable("MessageReadState");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.MessageID).HasColumnName("MessageID");
                builder.Property(t => t.UserID).HasColumnName("UserID");
                builder.Property(t => t.ReadDate).HasColumnName("ReadDate");
                builder.Property(t => t.Created).HasColumnName("Created");

                // Relationships
                builder.HasOne(t => t.AspNetUser)
                    .WithMany(t => t.MessageReadStates).IsRequired()
                    .HasForeignKey(d => d.UserID).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(t => t.Message)
                    .WithMany(t => t.MessageReadStates).IsRequired()
                    .HasForeignKey(d => d.MessageID);

            }
        }
    }
}
