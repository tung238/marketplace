using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasMaxLength(128);

            builder.HasOne(u => u.Region).WithMany(r => r.Users).IsRequired(false).HasForeignKey(u => u.RegionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.Area).WithMany(a => a.Users).IsRequired(false).HasForeignKey(u => u.AreaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
