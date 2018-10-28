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
using TNMarketplace.Core.Infrastructure;

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
            InstallCategories();
            InstallCategoryTypes();
            InstallSampleData(await _userManager.FindByEmailAsync("user@user.com"));
            InstallPictures();
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
             JsonConvert.DeserializeObject<Dictionary<string, Region>>(
               dataText, new JsonSerializerSettings
               {
                   ContractResolver = contractResolver,
                   Formatting = Formatting.Indented
               });
            foreach (var region in regions)
            {
                region.Value.ObjectState = ObjectState.Added;
                region.Value.ID = Convert.ToInt32(region.Key);
                
                _context.Regions.Add(region.Value);
            }
            //_context.SaveChanges();
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
                Name = "BeYourMarket",
                Description = "Find the beauty and spa service providers in your neighborhood!",
                Slogan = "BeYourMarket - Spa Demo",
                SearchPlaceHolder = "Search your Spa...",
                EmailContact = "hello@beyourmarket.com",
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
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
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
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
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
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
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
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                ObjectState = ObjectState.Added
            });
        }

        private void InstallSampleData(ApplicationUser user)
        {
            if (_context.Listings.Any())
            {
                return;
            }
            _context.Listings.Add(new Listing()
            {
                Title = "Preganancy Massage",
                Description = @"During an hour waiting women be allowed to experience total relaxation and relief of aches. The therapist works gently with the pregnant body to loosen up tight muscles, give peace to the nervous system, increase blood circulation and reduce pain in the body.",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 100,
                ContactName = "Celia",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Marievej 1, 2900 Hellerup, Danmark",
                Latitude = 55.730344,
                Longitude = 12.5767257,
                Expiration = DateTime.MaxValue.Date,
                Active = true,
                Enabled = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,
                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "Facial Treatment",
                Description = @"Classic 45 min Facial treat: Cleaning, skin analysis, AHA-PHA peeling, light deep cleanse...",
                CategoryID = 2,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 249,
                ContactName = "The Facial Lounge",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Sankt Jørgens Allé 5, København V, Danmark",
                Latitude = 55.6735479,
                Longitude = 12.559128399999963,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "60min Moisturizing face treatment",
                Description = @"During an hour waiting women be allowed to experience total relaxation and relief of aches. The therapist works gently with the pregnant body to loosen up tight muscles, give peace to the nervous system, increase blood circulation and reduce pain in the body.",
                CategoryID = 2,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 219,
                ContactName = "Clinique Margarita",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Studiestræde 18 København K",
                Latitude = 55.6786854,
                Longitude = 12.5694609,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "Eyelash extensions",
                Description = @"Give the lashes fullness and length with eyelash extensions. 50-100 fiber hair attached individually at one's own lashes for a natural look. The treatment takes about 90 minutes.",
                CategoryID = 2,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 375,
                ContactName = "Beauty And Accessories",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Henrik Steffens Vej 6, 1866 Frederiksberg C, Danmark",
                Latitude = 55.6779527,
                Longitude = 12.538388800000007,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "60min Massage for 2 persons - 3 types to choose",
                Description = @"Take your partner by the arm and enjoy an hour of relaxing parma sage. Choose freely between wellness, Aromatherapy- and hotstone massage. By wellness massage using long, smooth movements of the upper layers of the muscles of mental and physical relaxation. Fragrant oils from flowers and herbs used in Aroma Therapy massage for relaxation and enjoyment. By hotstone massage used heated lava rocks to smoothen the muscles, then loosen tensions and aches.",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 549,
                ContactName = "Healingstedet",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Liflandsgade 8 København 2300",
                Latitude = 55.6608952,
                Longitude = 12.6031471,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "1 hour shiatsu massage",
                Description = @"Let the body come into focus with an hour shiatsu massage that combines deep pressure and long runs. The treatment allows the body to sink into a relaxed state, and it is therefore suitable for stress-related genes and other long-term imbalances. The massage takes place on a mattress on the floor, and it is important to be dressed in comfortable clothes, so the body can completely relax.",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 249,
                ContactName = "Zen-Shiatsu",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Havnegade 43, st. th. København K 1058",
                Latitude = 55.677783,
                Longitude = 12.591222,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "Hot Stone Massage",
                Description = @"With heated lava stones are the body's tense muscles supple and ready for a deep massage. The massage works with aches and tension, and brings blood to the muscles for a pain-relieving effect and increased flexibility.",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 249,
                ContactName = "Healingstedet",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Liflandsgade 8, 2300 København",
                Latitude = 55.660869,
                Longitude = 12.603241,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "Facial massage",
                Description = @"30 mins facial massage",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 199,
                ContactName = "Natasha's Wellness",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Blegdamsvej 112A 2100 København",
                Latitude = 55.697579,
                Longitude = 12.574109,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });

            _context.Listings.Add(new Listing()
            {
                Title = "Waxing",
                Description = @"Choose one of below for waxing: arms / thighs / Lower armpit / Upper lip / Upper lip and chin",
                CategoryID = 3,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 249,
                ContactName = "Mind Body and Soul",
                ContactEmail = "demo@beyourmarket.com",
                IP = "",
                Location = "Blegdamsvej 84, St. tv , København Ø 2100",
                Latitude = 55.696199,
                Longitude = 12.571483,
                Active = true,
                Enabled = true,
                Expiration = DateTime.MaxValue.Date,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RegionId = 79,

                ObjectState = ObjectState.Added
            });
        }

        private void InstallListingTypes()
        {
            if (_context.ListingTypes.Any())
            {
                return;
            }
            _context.ListingTypes.Add(new ListingType()
            {
                Name = "Mua",
                ButtonLabel = "Đăng tin Mua",
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
                Name = "Bán",
                ButtonLabel = "Đăng tin Bán",
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
                Name = "Cho thuê",
                ButtonLabel = "Đăng tin Cho thuê",
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

        private void InstallCategories()
        {
            if (_context.Categories.Any())
            {
                return;
            }
            _context.Categories.Add(new Category()
            {
                Name = "Căn hộ / Chung cư",
                Description = "Căn hộ / Chung cư",
                Parent = 0,
                Enabled = true,
                Ordering = 0,
                ObjectState = ObjectState.Added
            });

            _context.Categories.Add(new Category()
            {
                Name = "Nhà ở",
                Description = "Nhà ở",
                Parent = 0,
                Enabled = true,
                Ordering = 1,
                ObjectState = ObjectState.Added
            });

            _context.Categories.Add(new Category()
            {
                Name = "Đất",
                Description = "Đất",
                Parent = 0,
                Enabled = true,
                Ordering = 2,
                ObjectState = ObjectState.Added
            });
            _context.Categories.Add(new Category()
            {
                Name = "Văn phòng / Mặt bằng kinh doanh",
                Description = "Văn phòng / Mặt bằng kinh doanh",
                Parent = 0,
                Enabled = true,
                Ordering = 3,
                ObjectState = ObjectState.Added
            });
        }

        private void InstallCategoryTypes()
        {
            if (_context.CategoryListingTypes.Any())
            {
                return;
            }
            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 1,
                ListingTypeID = 1,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 2,
                ListingTypeID = 1,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 3,
                ListingTypeID = 1,
                ObjectState = ObjectState.Added
            });
            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 4,
                ListingTypeID = 1,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 1,
                ListingTypeID = 2,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 2,
                ListingTypeID = 2,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 3,
                ListingTypeID = 2,
                ObjectState = ObjectState.Added
            });
            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 4,
                ListingTypeID = 2,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 1,
                ListingTypeID = 3,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 2,
                ListingTypeID = 3,
                ObjectState = ObjectState.Added
            });

            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 3,
                ListingTypeID = 3,
                ObjectState = ObjectState.Added
            });
            _context.CategoryListingTypes.Add(new CategoryListingType()
            {
                CategoryID = 4,
                ListingTypeID = 3,
                ObjectState = ObjectState.Added
            });
        }

        private void InstallPictures()
        {
            if (_context.Pictures.Any())
            {
                return;
            }
            for (int i = 1; i <= 9; i++)
            {
                _context.Pictures.Add(new Picture()
                {
                    MimeType = "image/jpeg",
                    ObjectState = ObjectState.Added
                });

                _context.SaveChanges();

                _context.ListingPictures.Add(new ListingPicture()
                {
                    ListingID = i,
                    PictureID = i,
                    ObjectState = ObjectState.Added
                });

                // Copy files
                //var pathFrom = Path.Combine(_hostingEnvironment.WebRootPath, "/images/sample/listing", string.Format("{0}.{1}", i.ToString("00000000"), "jpg"));
                //var pathTo = Path.Combine(_hostingEnvironment.WebRootPath, "/images/listing", string.Format("{0}.{1}", i.ToString("00000000"), "jpg"));
                //File.Copy(pathFrom, pathTo, true);
            }
        }
    }
}
