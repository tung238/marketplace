using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNMarketplace.Core;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Entities.Mapping;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.DataContext;

namespace TNMarketplace.Repository.EfCore
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IDataContextAsync
    {
        private readonly UserResolverService _userService;
        bool _disposed;
        //private readonly Guid _instanceId;

        public string CurrentUserId { get; internal set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationUserPhoto> ApplicationUserPhotos { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<Culture> Cultures { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        //public virtual DbSet<CategoryListingType> CategoryListingTypes { get; set; }
        public virtual DbSet<CategoryStat> CategoryStats { get; set; }
        public virtual DbSet<ContentPage> ContentPages { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<ListingMeta> ListingMetas { get; set; }
        public virtual DbSet<ListingPicture> ListingPictures { get; set; }
        public virtual DbSet<ListingReview> ListingReviews { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<ListingStat> ListingStats { get; set; }
        public virtual DbSet<ListingType> ListingTypes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageParticipant> MessageParticipants { get; set; }
        public virtual DbSet<MessageReadState> MessageReadStates { get; set; }
        public virtual DbSet<MessageThread> MessageThreads { get; set; }
        public virtual DbSet<MetaCategory> MetaCategories { get; set; }
        public virtual DbSet<MetaField> MetaFields { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<SettingDictionary> SettingDictionaries { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Area> Areas { get; internal set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserResolverService userService) : base(options)
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            //modelBuilder.ApplyConfiguration(new AspNetRoleMap());
            //modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            //modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.ApplyConfiguration(new ApplicationUserMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            //modelBuilder.ApplyConfiguration(new CategoryListingTypeMap());
            modelBuilder.ApplyConfiguration(new CategoryStatMap());
            modelBuilder.ApplyConfiguration(new ContentPageMap());
            modelBuilder.ApplyConfiguration(new EmailTemplateMap());
            modelBuilder.ApplyConfiguration(new ListingMetaMap());
            modelBuilder.ApplyConfiguration(new ListingPictureMap());
            modelBuilder.ApplyConfiguration(new ListingReviewMap());
            modelBuilder.ApplyConfiguration(new ListingMap());
            modelBuilder.ApplyConfiguration(new ListingStatMap());
            modelBuilder.ApplyConfiguration(new ListingTypeMap());
            modelBuilder.ApplyConfiguration(new MessageMap());
            modelBuilder.ApplyConfiguration(new MessageParticipantMap());
            modelBuilder.ApplyConfiguration(new MessageReadStateMap());
            modelBuilder.ApplyConfiguration(new MessageThreadMap());
            modelBuilder.ApplyConfiguration(new MetaCategoryMap());
            modelBuilder.ApplyConfiguration(new MetaFieldMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new SettingDictionaryMap());
            modelBuilder.ApplyConfiguration(new SettingMap());
            modelBuilder.ApplyConfiguration(new RegionMap());
            modelBuilder.ApplyConfiguration(new AreaMap());

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            //.Where(e => typeof(Entity).IsAssignableFrom(e.ClrType)))
            //{
            //    modelBuilder.Entity(entityType.ClrType)
            //        .Property<DateTime>("CreatedAt");

            //    modelBuilder.Entity(entityType.ClrType)
            //        .Property<DateTime>("UpdatedAt");

            //    modelBuilder.Entity(entityType.ClrType)
            //        .Property<string>("CreatedBy");

            //    modelBuilder.Entity(entityType.ClrType)
            //        .Property<string>("UpdatedBy");
            //}


            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        /// <summary>
        /// Override SaveChanges so we can call the new AuditEntities method.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            this.AuditEntities();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var validationErrors = ChangeTracker
                 .Entries<IValidatableObject>()
                 .SelectMany(e => e.Entity.Validate(null))
                 .Where(r => r != ValidationResult.Success);

                if (validationErrors.Any())
                {
                    // Possibly throw an exception here
                }

                SyncObjectsStatePreCommit();
                this.AuditEntities();
                var changesAsync = await base.SaveChangesAsync(cancellationToken);
                SyncObjectsStatePostCommit();
                return changesAsync;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity is IObjectState)
                {
                    dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
                }
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity is IObjectState)
                {
                    ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
                }
            }
        }

        public override void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }

            base.Dispose();
        }

        //public Guid InstanceId { get { return _instanceId; } }

        /// <summary>
        /// Method that will set the Audit properties for every added or modified Entity marked with the 
        /// IAuditable interface.
        /// </summary>
        private void AuditEntities()
        {

            DateTime now = DateTime.Now;
            // Get the authenticated user name 
            string userName = _userService.GetUser();

            //For every changed entity marked as IAditable set the values for the audit properties
            foreach (EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
                {
                    // If the entity was added.
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedBy").CurrentValue = userName;
                        entry.Property("CreatedAt").CurrentValue = now;
                    }
                    else if (entry.State == EntityState.Modified) // If the entity was updated
                    {
                        entry.Property("UpdatedBy").CurrentValue = userName;
                        entry.Property("UpdatedAt").CurrentValue = now;
                    }
                }
        }
    }
}
