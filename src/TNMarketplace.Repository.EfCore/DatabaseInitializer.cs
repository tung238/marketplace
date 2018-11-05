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
                Title = "Cho thuê biệt thự 2MT đường số 1 và 8A xã Bình Hưng BC",
                Description = @"Cho thuê biệt thự 2MT đường số 1 và 8A xã Bình Hưng BC",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 80000000,
                ContactName = "Tùng Trần",
                ContactEmail = "",
                ContactPhone = "938497498",
                IP = "",
                Location = "Đường số 1 Xã Bình Hưng",
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
                Title = "Cho thuê biệt thự đường 11, huyện BÌnh Chánh, trệt 2 lầu, 8 phòng",
                Description = @"Cho thuê biệt thự đường 11, huyện BÌnh Chánh, trệt 2 lầu, 8 phòng",
                CategoryID = 2,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 65000000,
                ContactName = "Phước Kiển",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "937217070",
                IP = "",
                Location = "Huyện Bình Chánh",
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
                Title = "Cho thuê biệt thự dường số KDC Trung Sơn, Bình Chánh",
                Description = @"Cho thuê biệt thự dường số KDC Trung Sơn, Bình Chánh",
                CategoryID = 2,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 38000000,
                ContactName = "Thiên Trường",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "936359596",
                IP = "",
                Location = "Huyện Bình Chánh",
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
                Title = "Bán biệt thự kdc phong phú ấp 5 dt 7x16m",
                Description = @"Bán biệt thự kdc phong phú ấp 5 dt 7x16m",
                CategoryID = 2,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 5000000000,
                ContactName = "Tân Định",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "909546419",
                IP = "",
                Location = "Đường số 16 Khu Dân Cư Ấp 5 Phong Phú",
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
                Title = "Chính thức mở bán khu biệt thự hưng thịnh villa tọa lạc quốc lộ 1A",
                Description = @"Chính thức mở bán khu biệt thự hưng thịnh villa tọa lạc quốc lộ 1A",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 5400000000,
                ContactName = "Nguyễn Đình Chiểu",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "939853312",
                IP = "",
                Location = "Nguyễn Hữu Trí",
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
                Title = "Ngân hàng ACB cần thanh toán 100 lock biệt thự SHR, 140m2",
                Description = @"Ngân hàng ACB cần thanh toán 100 lock biệt thự SHR, 140m2",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 2500000000,
                ContactName = "Zen-Shiatsu",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "939853312",
                IP = "",
                Location = "Trịnh Như Khuê Xã Bình Chánh",
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
                Title = "La Maison De Cần Giờ, bán 3 căn biệt thự liền kề 8 x32.Giá 3,9 tỷ/căn",
                Description = @"La Maison De Cần Giờ, bán 3 căn biệt thự liền kề 8 x32.Giá 3,9 tỷ/căn",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 3900000000,
                ContactName = "Healingstedet",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "975989959",
                IP = "",
                Location = "Huyện Cần Giờ",
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
                Title = "Đinh cư bán biệt thự vườn,6x20m2,4PN,Shr,MT đường 12m",
                Description = @"Đinh cư bán biệt thự vườn,6x20m2,4PN,Shr,MT đường 12m",
                CategoryID = 1,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 2000000,
                ContactName = "Lê Văn Lương",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "932683027",
                IP = "",
                Location = " Nguyễn Văn Bứa Xã Xuân Thới Sơn",
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
                Title = "Bán gấp Biệt Thự NineSouth. Căn góc 7x20m. Có sổ. Giá 9.5 tỷ.",
                Description = @"Bán gấp Biệt Thự NineSouth. Căn góc 7x20m. Có sổ. Giá 9.5 tỷ.",
                CategoryID = 3,
                ListingTypeID = 1,
                UserID = user.Id,
                Price = 2900000000,
                ContactName = "VinaCapital",
                ContactEmail = "demo@raodiaoc.com",
                ContactPhone = "904840402",
                IP = "",
                Location = "Nguyễn Hữu Thọ",
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
                Slug = "can-ho-chung-cu",
                Parent = 0,
                Enabled = true,
                Ordering = 0,
                ObjectState = ObjectState.Added
            });

            _context.Categories.Add(new Category()
            {
                Name = "Nhà ở",
                Description = "Nhà ở",
                Slug = "nha-o",
                Parent = 0,
                Enabled = true,
                Ordering = 1,
                ObjectState = ObjectState.Added
            });

            _context.Categories.Add(new Category()
            {
                Name = "Đất",
                Description = "Đất",
                Slug = "dat",
                Parent = 0,
                Enabled = true,
                Ordering = 2,
                ObjectState = ObjectState.Added
            });
            _context.Categories.Add(new Category()
            {
                Name = "Văn phòng / Mặt bằng kinh doanh",
                Description = "Văn phòng / Mặt bằng kinh doanh",
                Slug = "van-phong-mat-bang-kinh-doanh",
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
