using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Extensions;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.EfCore;

namespace TNMarketplace.ImportTool
{
    class Program
    {
        private static string _connectionString;

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public static ApplicationDbContext CreateDbContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(_connectionString);
            var service = new FakeUserResolverService(new FakeHttpContextAccessor());
            return new ApplicationDbContext(builder.Options, service);
        }
        static void Main(string[] args)
        {
            LoadConnectionString();
            var app = new CommandLineApplication();
            app.Name = "ConsoleArgs";
            app.Description = ".NET Core console app with argument parsing.";

            app.HelpOption("-?|-h|--help");

            var basicOption = app.Option("-p|--path",
                    "Duong dan den file text chua data",
                    CommandOptionType.SingleValue);
            app.OnExecute(() =>
            {
                if (basicOption.HasValue())
                {
                    var _context = CreateDbContext();
                    var user = _context.ApplicationUsers.FirstOrDefault(c => c.Email == "admin@admin.com");
                    if (user == null) {
                        Console.WriteLine("no user for email admin@admin.com, migrate database first??");
                    }
                    InstallSampleData(user, basicOption.Value());
                    Console.WriteLine("Insert data completed!!!");
                }
                else
                {
                    app.ShowHint();
                }

                return 0;
            });

            app.Command("simple-command", (command) =>
            {
                command.Description = "This is the description for simple-command.";
                command.HelpOption("-?|-h|--help");

                command.OnExecute(() =>
                {
                    Console.WriteLine("simple-command has finished.");
                    return 0;
                });
            });

            app.Execute(args);
        }

        public static void InstallSampleData(ApplicationUser user, string filePath)
        {
            var _context = CreateDbContext();
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open);
            }
            catch (Exception e)
            {
                return;
            }
            int count = 0;
            int countAll = 0;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                //read all categories, regions, areas
                var areas = _context.Areas.ToList();
                var regions = _context.Regions.ToList();
                var categories = _context.Categories.ToList();
                var listingTypes = _context.ListingTypes.ToList();
                string line = null;
                while ((line = reader.ReadLine()) != null)
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
                    string categorySlug = s[5].ConvertToSlug();
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
                    countAll++;
                    if (count >= 100)
                    {
                        _context.SaveChanges();
                        count = 0;
                    }
                    Console.WriteLine($"Xu ly xong {countAll} tin dang");
                }
            }
            //move file after complete
            //try
            //{
            //    File.Move(filePath, $"Migrations/chotot_{DateTime.Now.ToString("MM_dd_yyyy_HH_mm")}.txt");
            //}
            //catch (Exception e)
            //{
            //    return;
            //}
        }

    }
}
