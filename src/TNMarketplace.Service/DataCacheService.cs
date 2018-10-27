using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TNMarketplace.Service
{
    public class DataCacheService
    {
        private IServiceProvider ServiceProvider { get; set; }
        public MemoryCache MainCache { get; private set; }

        private object _lock = new object();

        public DataCacheService(IServiceProvider serviceProvider)
        {


            MainCache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024*5
            });

            GetCachedItem(CacheKeys.Settings);
            GetCachedItem(CacheKeys.SettingDictionary);
            GetCachedItem(CacheKeys.Categories);
            GetCachedItem(CacheKeys.ContentPages);
            GetCachedItem(CacheKeys.EmailTemplates);
            GetCachedItem(CacheKeys.Statistics);
        }

        public void UpdateCache(CacheKeys CacheKeyName, object CacheItem)
        {
            lock (_lock)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Set cache entry size by extension method.
                .SetSize(1)
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));
                //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00);

                // Add inside cache 
                MainCache.Set(CacheKeyName.ToString(), CacheItem, cacheEntryOptions);
            }
        }

        public object GetCachedItem(CacheKeys CacheKeyName)
        {
            lock (_lock)
            {
                if (!MainCache.TryGetValue(CacheKeyName.ToString(),out object result)) 
                {
                    switch (CacheKeyName)
                    {
                        case CacheKeys.Settings:
                            var setting = ServiceProvider.GetService<ISettingService>().Queryable().FirstOrDefault();
                            UpdateCache(CacheKeys.Settings, setting);
                            break;
                        case CacheKeys.SettingDictionary:
                            var settingDictionary = ServiceProvider.GetService<ISettingDictionaryService>().Queryable().ToList();
                            UpdateCache(CacheKeys.SettingDictionary, settingDictionary);
                            break;
                        case CacheKeys.Categories:
                            var categories = ServiceProvider.GetService < ICategoryService>().Queryable().Where(x => x.Enabled).OrderBy(x => x.Ordering).ToList();
                            UpdateCache(CacheKeys.Categories, categories);
                            break;
                        case CacheKeys.ListingTypes:
                            var ListingTypes = ServiceProvider.GetService < IListingTypeService>().Query().Include(x => x.CategoryListingTypes).Select().ToList();
                            UpdateCache(CacheKeys.ListingTypes, ListingTypes);
                            break;
                        case CacheKeys.ContentPages:
                            var contentPages = ServiceProvider.GetService < IContentPageService>().Queryable().Where(x => x.Published).OrderBy(x => x.Ordering).ToList();
                            UpdateCache(CacheKeys.ContentPages, contentPages);
                            break;
                        case CacheKeys.EmailTemplates:
                            var emailTemplates = ServiceProvider.GetService < IEmailTemplateService>().Queryable().ToList();
                            UpdateCache(CacheKeys.EmailTemplates, emailTemplates);
                            break;
                        case CacheKeys.Statistics:
                            SaveCategoryStats();

                            var statistics = new Statistics();
                            statistics.CategoryStats = ServiceProvider.GetService < ICategoryStatService>().Query().Include(x => x.Category).Select().ToList();

                            statistics.ListingCount = ServiceProvider.GetService < IListingService>().Queryable().Count();
                            //statistics.UserCount = AspNetUserService.Queryable().Count();
                            statistics.OrderCount = ServiceProvider.GetService < IOrderService>().Queryable().Count();
                            statistics.TransactionCount = 0;

                            statistics.ItemsCountDictionary = ServiceProvider.GetService < IListingService>().GetItemsCount(DateTime.Now.AddDays(-10));

                            UpdateCache(CacheKeys.Statistics, statistics);
                            break;
                        default:
                            break;
                    }
                };

                return result;
            }
        }

        // Update categories stats
        private void SaveCategoryStats()
        {
            var unitOfWorkAsync = ServiceProvider.GetService<IUnitOfWorkAsync>();

            var categoryCountDctionary = ServiceProvider.GetService<IListingService>().GetCategoryCount();

            foreach (var item in categoryCountDctionary)
            {
                var categoryStatService = ServiceProvider.GetService<ICategoryStatService>();
                var categoryStatQuery = categoryStatService.Query(x => x.CategoryID == item.Key.ID).Select();

                var categoryStat = categoryStatQuery.FirstOrDefault();

                if (categoryStat != null)
                {
                    categoryStat.Count = item.Value;
                    categoryStat.ObjectState = ObjectState.Modified;
                }
                else
                {
                    categoryStatService.Insert(new CategoryStat()
                    {
                        CategoryID = item.Key.ID,
                        Count = item.Value,
                        ObjectState = ObjectState.Added
                    });
                }
            }

            unitOfWorkAsync.SaveChanges();
        }

        public void RemoveCachedItem(CacheKeys CacheKeyName)
        {
            MainCache.Remove(CacheKeyName.ToString());
           
        }


        public Setting Settings
        {
            get
            {
                return GetCachedItem(CacheKeys.Settings) as Setting;
            }
        }

        public List<SettingDictionary> SettingDictionary
        {
            get
            {
                return GetCachedItem(CacheKeys.SettingDictionary) as List<SettingDictionary>;
            }
        }

        public List<Category> Categories
        {
            get
            {
                return GetCachedItem(CacheKeys.Categories) as List<Category>;
            }
        }

        public List<ListingType> ListingTypes
        {
            get
            {
                return GetCachedItem(CacheKeys.ListingTypes) as List<ListingType>;
            }
        }

        public List<ContentPage> ContentPages
        {
            get
            {
                return GetCachedItem(CacheKeys.ContentPages) as List<ContentPage>;
            }
        }

        public SettingDictionary GetSettingDictionary(string settingKey)
        {
            var setting = SettingDictionary.Where(x => x.Name == settingKey).FirstOrDefault();

            if (setting == null)
                return new SettingDictionary()
                {
                    Name = settingKey.ToString(),
                    Value = string.Empty
                };

            return setting;
        }

        public Statistics Statistics
        {
            get
            {
                return GetCachedItem(CacheKeys.Statistics) as Statistics;
            }
        }

    }
}
