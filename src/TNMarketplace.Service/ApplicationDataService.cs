using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNMarketplace.Core;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.ViewModels;
using TNMarketplace.Repository.EfCore.JsonModel;

namespace TNMarketplace.Service
{
    public class ApplicationDataService : IApplicationDataService
    {
        private readonly IOptions<RequestLocalizationOptions> _locOptions;
        private readonly IHttpContextAccessor _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<ApplicationDataService> _stringLocalizer;
        private readonly IMemoryCache _cache;
        private readonly DataCacheService _dataCacheService;
        private readonly IMapper _mapper;

        public ApplicationDataService(
            IOptions<RequestLocalizationOptions> locOptions,
            IHttpContextAccessor context,
            DataCacheService dataCacheService,
            SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<ApplicationDataService> stringLocalizer,
            IMemoryCache memoryCache,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _locOptions = locOptions;
            _context = context;
            _signInManager = signInManager;
            _stringLocalizer = stringLocalizer;
            _cache = memoryCache;
            _dataCacheService = dataCacheService;
        }

        public async Task<object> GetApplicationData(HttpContext context)
        {
            var data = "";
            if (!_cache.TryGetValue("GetApplicationData", out data))
            {
                var regions = _dataCacheService.Regions;

                var regionsTree = new List<TreeItem>();
                foreach(var r in regions.OrderBy(r=>r.Ordering).ThenBy(r=>r.Slug))
                {
                    var rItem = new TreeItem
                    {
                        ID = r.ID,
                        Name = r.Name,
                        Slug = r.Slug,
                        NameWithType = r.NameWithType,
                        Children = new List<TreeItem>()
                    };
                    foreach(var a in r.Areas.OrderBy(a=>a.Ordering).ThenBy(a=>a.Slug))
                    {
                        var aItem = new TreeItem
                        {
                          
                                ID = a.ID,
                                Name = a.Name,
                                NameWithType = a.NameWithType,
                                Slug = a.Slug,
                           
                            IsLeaf = true
                        };
                        rItem.Children.Add(aItem);
                    }
                    regionsTree.Add(rItem);

                }
                var categoriesTree = new List<TreeItem>();
                var categories = _dataCacheService.Categories;
                foreach(var cat in categories.Where(c=>c.Parent == 0 ).OrderBy(c=>c.Ordering).ThenBy(c=>c.Slug))
                {
                    var cItem = new TreeItem
                    {
                        IsLeaf = false,
                        ID = cat.ID,
                        Name = cat.Name,
                        Slug = cat.Slug,
                        IconClass = cat.IconClass,
                        MaxPrice = cat.MaxPrice,
                        Children = new List<TreeItem>()
                    };
                    foreach(var child in categories.Where(c=>c.Parent == cat.ID).OrderBy(c=>c.Ordering))
                    {
                        var childItem = new TreeItem
                        {
                            IsLeaf = true,
                            ID = child.ID,
                            Name = child.Name,
                            Slug = child.Slug,
                            IconClass = child.IconClass,
                            MaxPrice = child.MaxPrice,
                            Children = new List<TreeItem>()
                        };
                        cItem.Children.Add(childItem);
                    }
                    categoriesTree.Add(cItem);
                }
                data = Helpers.JsonSerialize(new
                {
                    Content = GetContentByCulture(context),
                    CookieConsent = GetCookieConsent(context),
                    Cultures = _locOptions.Value.SupportedUICultures
                            .Select(c => new { Value = c.Name, Text = c.DisplayName, Current = (c.Name == Thread.CurrentThread.CurrentCulture.Name) })
                            .ToList(),
                    LoginProviders = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList().Select(a => a.Name),
                    CategoriesTree = categoriesTree,
                    RegionsTree = regionsTree,
                    ListingTypes = _mapper.Map<List<SimpleListingType>>(_dataCacheService.ListingTypes)
                });
                _cache.Set("GetApplicationData", data);
            }
            return data;
        }

        private Dictionary<string, string> GetContentByCulture(HttpContext context)
        {
            var requestCulture = context.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = requestCulture.RequestCulture.Culture;

            var CACHE_KEY = $"Content-{culture.Name}";


            Dictionary<string, string> cacheEntry;

            // Look for cache key.
            if (!_cache.TryGetValue(CACHE_KEY, out cacheEntry))
            {
                // Key not in cache, so get & set data.
                var culturalContent = _stringLocalizer.WithCulture(culture)
                                        .GetAllStrings()
                                        .Select(c => new ContentVm
                                        {
                                            Key = c.Name,
                                            Value = c.Value
                                        })
                                        .ToDictionary(x => x.Key, x => x.Value);
                cacheEntry = culturalContent;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                // Save data in cache.
                _cache.Set(CACHE_KEY, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        private object GetCookieConsent(HttpContext httpContext)
        {
            var consentFeature = httpContext.Features.Get<ITrackingConsentFeature>();
            var showConsent = false;//!consentFeature?.CanTrack ?? false;
            var cookieString = consentFeature?.CreateConsentCookie();
            return new { showConsent, cookieString };
        }
    }
}
