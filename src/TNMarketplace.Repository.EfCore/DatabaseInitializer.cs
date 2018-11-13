using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenIddict.Core;
using OpenIddict.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TNMarketplace.Core;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Extensions;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.EfCore.JsonModel;

namespace TNMarketplace.Repository.EfCore
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync(IConfiguration configuration);
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _openIddictApplicationManager;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DatabaseInitializer(
            ApplicationDbContext context,
            ILogger<DatabaseInitializer> logger,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnvironment
            )
        {
            _context = context;
            _logger = logger;
            _openIddictApplicationManager = openIddictApplicationManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            CreateRoles();
            CreateUsers();
            AddLocalisedData();
            await AddOpenIdConnectOptions(configuration);
            InstallSettings();
            InstallEmailTemplates();
            InstallListingTypes();
            InstallRegions();
            InstallAreas();
            //InstallCategories();
            InstallSampleData(await _userManager.FindByEmailAsync("user@user.com"));
            //InstallPictures();
        }


        private void CreateRoles()
        {
            var rolesToAdd = new List<ApplicationRole>(){
                new ApplicationRole { Name= "Admin", Description = "Full rights role"},
                new ApplicationRole { Name= "User", Description = "Limited rights role"}
            };
            foreach (var role in rolesToAdd)
            {
                if (!_roleManager.RoleExistsAsync(role.Name).Result)
                {
                    _roleManager.CreateAsync(role).Result.ToString();
                }
            }
        }
        private void CreateUsers()
        {
            if (!_context.ApplicationUsers.Any())
            {
                var adminUser = new ApplicationUser { UserName = "admin@admin.com", FirstName = "Admin first", LastName = "Admin last", Email = "admin@admin.com", Mobile = "0123456789", EmailConfirmed = true, CreatedDate = DateTime.Now, IsEnabled = true };
                _userManager.CreateAsync(adminUser, "P@ssw0rd!").Result.ToString();
                _userManager.AddClaimAsync(adminUser, new Claim(OpenIdConnectConstants.Claims.PhoneNumber, adminUser.Mobile.ToString(), ClaimValueTypes.Integer)).Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("admin@admin.com").GetAwaiter().GetResult(), "Admin").Result.ToString();

                var normalUser = new ApplicationUser { UserName = "user@user.com", FirstName = "First", LastName = "Last", Email = "user@user.com", Mobile = "0123456789", EmailConfirmed = true, CreatedDate = DateTime.Now, IsEnabled = true };
                _userManager.CreateAsync(normalUser, "P@ssw0rd!").Result.ToString();
                _userManager.AddClaimAsync(adminUser, new Claim(OpenIdConnectConstants.Claims.PhoneNumber, adminUser.Mobile.ToString(), ClaimValueTypes.Integer)).Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("user@user.com").GetAwaiter().GetResult(), "User").Result.ToString();
            }
        }
        private void AddLocalisedData()
        {
            if (!_context.Cultures.Any())
            {
                var translations = _hostingEnvironment.GetTranslationFile();

                var locales = translations.First().Split(",").Skip(1).ToList();

                var currentLocale = 0;

                locales.ForEach(locale =>
                {
                    currentLocale += 1;

                    var culture = new Culture
                    {
                        Name = locale
                    };
                    var resources = new List<Resource>();
                    translations.Skip(1).ToList().ForEach(t =>
                    {
                        var line = t.Split(",");
                        resources.Add(new Resource
                        {
                            Culture = culture,
                            Key = line[0],
                            Value = line[currentLocale]
                        });
                    });

                    culture.Resources = resources;

                    _context.Cultures.Add(culture);

                    _context.SaveChanges();
                });
            }

        }

        private async Task AddOpenIdConnectOptions(IConfiguration configuration)
        {
            if (await _openIddictApplicationManager.FindByClientIdAsync("TNMarketplace") == null)
            {
                var host = configuration["HostUrl"].ToString();

                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "TNMarketplace",
                    DisplayName = "TNMarketplace",
                    PostLogoutRedirectUris = { new Uri($"{host}signout-oidc") },
                    RedirectUris = { new Uri(host) },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Implicit,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                    }
                };

                await _openIddictApplicationManager.CreateAsync(descriptor);
            }

        }

        private void InstallRegions()
        {
            if (_context.Regions.Any())
            {
                return;
            }
            var dataText = System.IO.File.ReadAllText(@"Migrations/region_list.json");
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            var regions =
             JsonConvert.DeserializeObject<Dictionary<string, RegionJsonModel>>(
               dataText, new JsonSerializerSettings
               {
                   ContractResolver = contractResolver,
                   Formatting = Formatting.Indented
               });
            foreach (var regionJson in regions)
            {
                var region = new Region
                {
                    ID = Convert.ToInt32(regionJson.Key),
                    Name = regionJson.Value.Name,
                    Type = regionJson.Value.Type,
                    Slug = regionJson.Value.Slug,
                    NameWithType = regionJson.Value.NameWithType,
                    ObjectState = ObjectState.Added
                };
                
                _context.Regions.Add(region);
            }
            _context.SaveChanges();
        }

        private void InstallAreas()
        {
            if (_context.Areas.Any())
            {
                return;
            }
            var dataText = System.IO.File.ReadAllText(@"Migrations/area_list.json");
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            var areas =
             JsonConvert.DeserializeObject<Dictionary<string, AreaJsonModel>>(
               dataText, new JsonSerializerSettings
               {
                   ContractResolver = contractResolver,
                   Formatting = Formatting.Indented
               });
            foreach (var areaJson in areas)
            {
                var area = new Area
                {
                    ID = Convert.ToInt32(areaJson.Key),
                    Name = areaJson.Value.Name,
                    NameWithType = areaJson.Value.NameWithType,
                    ObjectState = ObjectState.Added,
                    Path = areaJson.Value.Path,
                    PathWithType = areaJson.Value.PathWithType,
                    RegionId = Convert.ToInt32(areaJson.Value.ParentCode),
                    Slug = areaJson.Value.Slug,
                    Type = areaJson.Value.Type
                };
                _context.Areas.Add(area);
            }
            _context.SaveChanges();
        }

            private void InstallSettings()
        {
            if (_context.Settings.Any())
            {
                return;
            }
            _context.Settings.Add(new Setting()
            {
                ID = 1,
                Name = "Rao địa ốc",
                Description = "Đăng tin mua bán, cho thuê bất động sản toàn quốc",
                Slogan = "Đăng tin bán bất động sản được ngay",
                SearchPlaceHolder = "Tìm sản phẩm mong muốn",
                EmailContact = "support@raodiaoc.com",
                Version = "1.0",
                TransactionFeePercent = 1,
                TransactionMinimumSize = 10,
                TransactionMinimumFee = 10,
                EmailConfirmedRequired = false,
                Theme = "Default",
                DateFormat = DateTimeFormatInfo.CurrentInfo.ShortDatePattern,
                TimeFormat = DateTimeFormatInfo.CurrentInfo.ShortTimePattern,
                ListingReviewEnabled = true,
                ListingReviewMaxPerDay = 5,
                ObjectState = ObjectState.Added
            });

            // Copy files
            //var files = new string[] { "cover.jpg", "favicon.jpg", "logo.png" };
            //foreach (var file in files)
            //{
            //    var pathFrom = Path.Combine(_hostingEnvironment.WebRootPath, "/images/sample/community", file);
            //    var pathTo = Path.Combine(_hostingEnvironment.WebRootPath, "/images/community", file);
            //    File.Copy(pathFrom, pathTo, true);
            //}
        }

        private void InstallEmailTemplates()
        {
            if (_context.EmailTemplates.Any())
            {
                return;
            }
            _context.EmailTemplates.Add(new EmailTemplate()
            {
                Slug = "signup",
                Subject = "Sign up",
                Body = @"<p>Hi there,</p>
                        <h1>Welcome to {SiteName}</h1>
                        <p>Thanks for your sign up.</p>
                        <table>
	                        <tbody>
		                        <tr>
			                        <td class=""padding"">
			                        <p><a class=""btn-primary"" href=""{CallbackUrl}"">Please confirm your email by clicking this link</a></p>
			                        </td>
		                        </tr>
	                        </tbody>
                        </table>",
                SendCopy = true,
                ObjectState = ObjectState.Added
            });

            _context.EmailTemplates.Add(new EmailTemplate()
            {
                Slug = "forgotpassword",
                Subject = "Forgot Password",
                Body = @"<p>Hi there,</p>
                        <p>You can use the link below to reset your password.</p>
                        <table>
	                        <tbody>
		                        <tr>
			                        <td class=""padding"">
			                        <p><a class=""btn-primary"" href=""{CallbackUrl}"">Please reset your password by clicking this link</a></p>
			                        </td>
		                        </tr>
	                        </tbody>
                        </table>",
                SendCopy = true,
                ObjectState = ObjectState.Added
            });

            _context.EmailTemplates.Add(new EmailTemplate()
            {
                Slug = "privatemessage",
                Subject = "Private Message",
                Body = @"<p>Hi there,</p>
			            <p>You got a new message as below.</p>
			            <table>
				            <tbody>
					            <tr>
						            <td class=""padding"">
						            <h4>{Message}</h4>
						            </td>
					            </tr>
				            </tbody>
			            </table>",
                SendCopy = true,
                ObjectState = ObjectState.Added
            });
        }

        private void InstallSampleData(ApplicationUser user)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream("Migrations/chotot.txt", FileMode.Open);
            }catch (Exception e)
            {
                return;
            }
            int count = 0;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                //read all categories, regions, areas
                var areas = _context.Areas.ToList();
                var regions = _context.Regions.ToList();
                var categories = _context.Categories.ToList();
                var listingTypes = _context.ListingTypes.ToList();
                string line = null;
                while((line = reader.ReadLine()) != null)
                {
                    var s = line.Split("$$", StringSplitOptions.None);
                    if (s.Length != 8)
                    {
                        continue;
                    }
                    var listing = new Listing
                    {
                        //id$$title$$body$$categoryName$$District$$Province$$Phone$$Price
                        Active = true,
                        UserID = user.Id,
                        ContactEmail = "",
                        ContactName = "",
                        ContactPhone = s[6],
                        Title = s[1],
                        Description = s[2],
                        Enabled = true,
                        IP = "",
                        ObjectState = ObjectState.Added,
                        ShowPhone = true,
                        Expiration = DateTime.Now.AddMonths(3),
                        Latitude = null,
                        Longitude = null,
                        Location = "",
                        Price = s[7].GetDoubleValue(),
                        ShowEmail = true,
                        ExternalId = Convert.ToInt32(s[0])
                    };
                    if (_context.Listings.FirstOrDefault(l => l.ExternalId == listing.ExternalId) != null)
                    {
                        continue;
                    }
                    var categorySlug = s[5].ConvertToSlug();
                    var regionslug = s[4].ConvertToSlug();
                    var areaName = s[3];
                    var cat = categories.FirstOrDefault(c => c.Slug.Equals(categorySlug, StringComparison.OrdinalIgnoreCase));
                    if (cat == null)
                    {
                        cat = new Category
                        {
                            Name = s[5],
                            Slug = categorySlug,
                            Enabled = true,
                            ObjectState = ObjectState.Added,
                            Description = s[5],
                            Ordering = categories.Count,
                            Parent = 0
                        };
                        _context.Categories.Add(cat);
                        _context.SaveChanges();
                        categories = _context.Categories.ToList();
                        cat = categories.First(c => c.Slug.Equals(categorySlug, StringComparison.OrdinalIgnoreCase));
                    }
                    listing.CategoryID = cat.ID;
                    var region = regions.First(r => r.Slug.Equals(regionslug, StringComparison.OrdinalIgnoreCase));
                    listing.RegionId = region.ID;

                    var listingType = listingTypes.First();
                    var listingTitleUnsign = listing.Title.ConvertToUnSign();
                    if (listingTitleUnsign.Contains("thue"))
                    {
                        listingType = listingTypes.First(l => l.Name.ConvertToUnSign().Contains("thue"));
                    }
                    else if (listingTitleUnsign.Contains("mua"))
                    {
                        listingType = listingTypes.First(l => l.Name.ConvertToUnSign().Contains("mua"));
                    }
                    listing.ListingTypeID = listingType.ID;
                    var area = areas.FirstOrDefault(a => a.NameWithType.ConvertToUnSign().Equals(areaName.ConvertToUnSign()));
                    if (area != null)
                    {
                        listing.AreaId = area.ID;
                    }
                    _context.Listings.Add(listing);
                    count++;
                    if (count >= 100)
                    {
                        _context.SaveChanges();
                        count = 0;
                    }
                }
            }
            //move file after complete
            try
            {
                File.Move("Migrations/chotot.txt", $"Migrations/chotot_{DateTime.Now.ToString("MM_dd_yyyy_HH_mm")}.txt");
            }
            catch (Exception e)
            {
                return;
            }
        }

        private void InstallListingTypes()
        {
            if (_context.ListingTypes.Any())
            {
                return;
            }
            _context.ListingTypes.Add(new ListingType()
            {
                Name = "Bán",
                ButtonLabel = "Đăng tin Bán",
                Slug = "ban",
                OrderTypeID = (int)Enum_ListingOrderType.None,
                OrderTypeLabel = "Mua",
                PriceUnitLabel = "",
                PaymentEnabled = true,
                PriceEnabled = true,
                ShippingEnabled = false,
                ObjectState = ObjectState.Added
            });
            _context.ListingTypes.Add(new ListingType()
            {
                Name = "Mua",
                ButtonLabel = "Đăng tin Mua",
                OrderTypeID = (int)Enum_ListingOrderType.None,
                OrderTypeLabel = "Mua",
                Slug = "mua",
                PriceUnitLabel = "",
                PaymentEnabled = true,
                PriceEnabled = true,
                ShippingEnabled = false,
                ObjectState = ObjectState.Added
            });
            _context.ListingTypes.Add(new ListingType()
            {
                Name = "Cho thuê",
                ButtonLabel = "Đăng tin Cho thuê",
                Slug = "cho_thue",
                OrderTypeID = (int)Enum_ListingOrderType.None,
                OrderTypeLabel = "Thuê",
                PriceUnitLabel = "",
                PaymentEnabled = true,
                PriceEnabled = true,
                ShippingEnabled = false,
                ObjectState = ObjectState.Added
            });


            _context.SaveChanges();
        }

        //private void InstallCategories()
        //{
        //    if (_context.Categories.Any())
        //    {
        //        return;
        //    }
        //    _context.Categories.Add(new Category()
        //    {
        //        Name = "Căn hộ / Chung cư",
        //        Description = "Căn hộ / Chung cư",
        //        Slug = "can-ho-chung-cu",
        //        Parent = 0,
        //        Enabled = true,
        //        Ordering = 0,
        //        ObjectState = ObjectState.Added
        //    });

        //    _context.Categories.Add(new Category()
        //    {
        //        Name = "Nhà ở",
        //        Description = "Nhà ở",
        //        Slug = "nha-o",
        //        Parent = 0,
        //        Enabled = true,
        //        Ordering = 1,
        //        ObjectState = ObjectState.Added
        //    });

        //    _context.Categories.Add(new Category()
        //    {
        //        Name = "Đất",
        //        Description = "Đất",
        //        Slug = "dat",
        //        Parent = 0,
        //        Enabled = true,
        //        Ordering = 2,
        //        ObjectState = ObjectState.Added
        //    });
        //    _context.Categories.Add(new Category()
        //    {
        //        Name = "Văn phòng / Mặt bằng kinh doanh",
        //        Description = "Văn phòng / Mặt bằng kinh doanh",
        //        Slug = "van-phong-mat-bang-kinh-doanh",
        //        Parent = 0,
        //        Enabled = true,
        //        Ordering = 3,
        //        ObjectState = ObjectState.Added
        //    });
        //}

        //private void InstallPictures()
        //{
        //    if (_context.Pictures.Any())
        //    {
        //        return;
        //    }
        //    for (int i = 1; i <= 9; i++)
        //    {
        //        _context.Pictures.Add(new Picture()
        //        {
        //            MimeType = "image/jpeg",
        //            ObjectState = ObjectState.Added
        //        });

        //        _context.SaveChanges();

        //        _context.ListingPictures.Add(new ListingPicture()
        //        {
        //            ListingID = i,
        //            PictureID = i,
        //            ObjectState = ObjectState.Added
        //        });

        //        // Copy files
        //        //var pathFrom = Path.Combine(_hostingEnvironment.WebRootPath, "/images/sample/listing", string.Format("{0}.{1}", i.ToString("00000000"), "jpg"));
        //        //var pathTo = Path.Combine(_hostingEnvironment.WebRootPath, "/images/listing", string.Format("{0}.{1}", i.ToString("00000000"), "jpg"));
        //        //File.Copy(pathFrom, pathTo, true);
        //    }
        //}
    }
}
