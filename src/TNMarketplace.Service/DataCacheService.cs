﻿using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TNMarketplace.Service
{
    public class DataCacheService
    {
        private readonly IServiceProvider _serviceProvider;
        public MemoryCache MainCache { get; private set; }

        private object _lock = new object();

        public DataCacheService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

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
                            var setting = _serviceProvider.GetService<ISettingService>().Queryable().FirstOrDefault();
                            UpdateCache(CacheKeys.Settings, setting);
                            return setting;
                            
                        case CacheKeys.SettingDictionary:
                            var settingDictionary = _serviceProvider.GetService<ISettingDictionaryService>().Queryable().ToList();
                            UpdateCache(CacheKeys.SettingDictionary, settingDictionary);
                            return settingDictionary;
                        case CacheKeys.Categories:
                            var categories = _serviceProvider.GetService < ICategoryService>().Queryable().Where(x => x.Enabled).OrderBy(x => x.Ordering).ToList();
                            UpdateCache(CacheKeys.Categories, categories);
                            return categories;
                        case CacheKeys.ListingTypes:
                            var ListingTypes = _serviceProvider.GetService < IListingTypeService>().Query().Include(x => x.Include(y=> y.CategoryListingTypes)).Select().ToList();
                            UpdateCache(CacheKeys.ListingTypes, ListingTypes);
                            return ListingTypes;
                        case CacheKeys.ContentPages:
                            var contentPages = _serviceProvider.GetService < IContentPageService>().Queryable().Where(x => x.Published).OrderBy(x => x.Ordering).ToList();
                            UpdateCache(CacheKeys.ContentPages, contentPages);
                            return contentPages;
                        case CacheKeys.EmailTemplates:
                            var emailTemplates = _serviceProvider.GetService < IEmailTemplateService>().Queryable().ToList();
                            UpdateCache(CacheKeys.EmailTemplates, emailTemplates);
                            return emailTemplates;
                        case CacheKeys.Regions:
                            var regions = _serviceProvider.GetService<IRegionService>().Queryable().ToList();
                            UpdateCache(CacheKeys.Regions, regions);
                            return regions;
                        case CacheKeys.Statistics:
                            SaveCategoryStats();

                            var statistics = new Statistics();
                            statistics.CategoryStats = _serviceProvider.GetService < ICategoryStatService>().Query()
                                .Include(x => x.Include(y => y.Category)).Select().ToList();

                            statistics.ListingCount = _serviceProvider.GetService < IListingService>().Queryable().Count();
                            //statistics.UserCount = AspNetUserService.Queryable().Count();
                            statistics.OrderCount = _serviceProvider.GetService < IOrderService>().Queryable().Count();
                            statistics.TransactionCount = 0;

                            statistics.ItemsCountDictionary = _serviceProvider.GetService < IListingService>().GetItemsCount(DateTime.Now.AddDays(-10));

                            UpdateCache(CacheKeys.Statistics, statistics);
                            return statistics;
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
            var unitOfWorkAsync = _serviceProvider.GetService<IUnitOfWorkAsync>();

            var categoryCountDctionary = _serviceProvider.GetService<IListingService>().GetCategoryCount();

            foreach (var item in categoryCountDctionary)
            {
                var categoryStatService = _serviceProvider.GetService<ICategoryStatService>();
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

        public List<Region> Regions
        {
            get
            {
                return GetCachedItem(CacheKeys.Regions) as List<Region>;
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
