using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities.Mapping
{
    public class SettingMap : IEntityTypeConfiguration<Setting>
    {

        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            {
                // Primary Key
                builder.HasKey(t => t.ID);

                // Properties
                builder.Property(t => t.ID).ValueGeneratedNever();

                builder.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(t => t.Description)
                    .IsRequired();

                builder.Property(t => t.Slogan)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(t => t.SearchPlaceHolder)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(t => t.EmailContact)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(t => t.Version)
                    .IsRequired()
                    .HasMaxLength(10);

                builder.Property(t => t.Currency)
                    .IsRequired()
                    .IsFixedLength()
                    .HasMaxLength(3);

                builder.Property(t => t.SmtpHost)
                    .HasMaxLength(100);

                builder.Property(t => t.SmtpUserName)
                    .HasMaxLength(100);

                builder.Property(t => t.SmtpPassword)
                    .HasMaxLength(100);

                builder.Property(t => t.EmailAddress)
                    .HasMaxLength(100);

                builder.Property(t => t.EmailDisplayName)
                    .HasMaxLength(100);

                builder.Property(t => t.AgreementLabel)
                    .HasMaxLength(100);

                builder.Property(t => t.Theme)
                    .IsRequired()
                    .HasMaxLength(250);

                builder.Property(t => t.DateFormat)
                    .IsRequired()
                    .HasMaxLength(10);

                builder.Property(t => t.TimeFormat)
                    .IsRequired()
                    .HasMaxLength(10);

                builder.Property(t => t.ListingReviewEnabled)
                    .IsRequired();

                builder.Property(t => t.ListingReviewMaxPerDay)
                    .IsRequired();

                // Table & Column Mappings
                builder.ToTable("Settings");
                builder.Property(t => t.ID).HasColumnName("ID");
                builder.Property(t => t.Name).HasColumnName("Name");
                builder.Property(t => t.Description).HasColumnName("Description");
                builder.Property(t => t.Slogan).HasColumnName("Slogan");
                builder.Property(t => t.SearchPlaceHolder).HasColumnName("SearchPlaceHolder");
                builder.Property(t => t.EmailContact).HasColumnName("EmailContact");
                builder.Property(t => t.Version).HasColumnName("Version");
                builder.Property(t => t.Currency).HasColumnName("Currency");
                builder.Property(t => t.TransactionFeePercent).HasColumnName("TransactionFeePercent");
                builder.Property(t => t.TransactionMinimumSize).HasColumnName("TransactionMinimumSize");
                builder.Property(t => t.TransactionMinimumFee).HasColumnName("TransactionMinimumFee");
                builder.Property(t => t.SmtpHost).HasColumnName("SmtpHost");
                builder.Property(t => t.SmtpPort).HasColumnName("SmtpPort");
                builder.Property(t => t.SmtpUserName).HasColumnName("SmtpUserName");
                builder.Property(t => t.SmtpPassword).HasColumnName("SmtpPassword");
                builder.Property(t => t.SmtpSSL).HasColumnName("SmtpSSL");
                builder.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
                builder.Property(t => t.EmailDisplayName).HasColumnName("EmailDisplayName");
                builder.Property(t => t.AgreementRequired).HasColumnName("AgreementRequired");
                builder.Property(t => t.AgreementLabel).HasColumnName("AgreementLabel");
                builder.Property(t => t.AgreementText).HasColumnName("AgreementText");
                builder.Property(t => t.SignupText).HasColumnName("SignupText");
                builder.Property(t => t.EmailConfirmedRequired).HasColumnName("EmailConfirmedRequired");
                builder.Property(t => t.Theme).HasColumnName("Theme");
                builder.Property(t => t.DateFormat).HasColumnName("DateFormat");
                builder.Property(t => t.TimeFormat).HasColumnName("TimeFormat");
                builder.Property(t => t.ListingReviewEnabled).HasColumnName("ListingReviewEnabled");
                builder.Property(t => t.ListingReviewMaxPerDay).HasColumnName("ListingReviewMaxPerDay");
                builder.Property(t => t.Created).HasColumnName("Created");
                builder.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            }
        }
    }
}
