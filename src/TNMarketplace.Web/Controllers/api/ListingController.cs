﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TNMarketplace.Core;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.Repositories;
using TNMarketplace.Repository.UnitOfWork;
using TNMarketplace.Service;
using TNMarketplace.Web.Extensions;
using TNMarketplace.Web.Models;
using TNMarketplace.Web.Utilities;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TNMarketplace.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize]
    public class ListingController : BaseController
    {
        #region Fields
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        private readonly ISettingService _settingService;
        private readonly ISettingDictionaryService _settingDictionaryService;
        private readonly ICategoryService _categoryService;

        private readonly IListingService _listingService;
        private readonly IListingStatService _ListingStatservice;
        private readonly IListingPictureService _listingPictureservice;
        private readonly IListingReviewService _listingReviewService;

        private readonly IPictureService _pictureService;
        private readonly IOrderService _orderService;
        private readonly ICustomFieldService _customFieldService;
        private readonly ICustomFieldCategoryService _customFieldCategoryService;
        private readonly ICustomFieldListingService _customFieldListingService;

        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMessageService _messageService;
        private readonly IMessageThreadService _messageThreadService;
        private readonly IMessageParticipantService _messageParticipantService;
        private readonly IMessageReadStateService _messageReadStateService;

        private readonly DataCacheService _dataCacheService;
        private readonly SqlDbService _sqlDbService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;
        private readonly ImageHelper _imageHelper;
        private readonly IMapper _mapper;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        #endregion

        #region Contructors
        public ListingController(
           IUnitOfWorkAsync unitOfWorkAsync,
           ISettingService settingService,
           ICategoryService categoryService,
           IListingService listingService,
           IPictureService pictureService,
           IListingPictureService listingPictureservice,
           IOrderService orderService,
           ICustomFieldService customFieldService,
           ICustomFieldCategoryService customFieldCategoryService,
           ICustomFieldListingService customFieldListingService,
           ISettingDictionaryService settingDictionaryService,
           IListingStatService listingStatservice,
            IListingReviewService listingReviewService,
           IEmailTemplateService emailTemplateService,
           IMessageService messageService,
            IMessageThreadService messageThreadService,
           IMessageParticipantService messageParticipantService,
           IMessageReadStateService messageReadStateService,
           UserManager<ApplicationUser> userManager,
           DataCacheService dataCacheService,
           IHostingEnvironment environment,
           ImageHelper imageHelper,
           IMapper mapper,

        SqlDbService sqlDbService)
        {
            _settingService = settingService;
            _settingDictionaryService = settingDictionaryService;

            _categoryService = categoryService;
            _listingService = listingService;
            _listingReviewService = listingReviewService;
            _pictureService = pictureService;
            _listingPictureservice = listingPictureservice;
            _orderService = orderService;
            _customFieldService = customFieldService;
            _customFieldCategoryService = customFieldCategoryService;
            _customFieldListingService = customFieldListingService;
            _ListingStatservice = listingStatservice;
            _emailTemplateService = emailTemplateService;
            _messageService = messageService;
            _messageParticipantService = messageParticipantService;
            _messageReadStateService = messageReadStateService;
            _messageThreadService = messageThreadService;
            _dataCacheService = dataCacheService;
            _sqlDbService = sqlDbService;
            _userManager = userManager;
            _environment = environment;
            _imageHelper = imageHelper;
            _mapper = mapper;

        _unitOfWorkAsync = unitOfWorkAsync;
        }
        #endregion

        #region Methods
        //http://stackoverflow.com/questions/11774741/load-partial-view-depending-on-dropdown-selection-in-mvc3
        [HttpGet("ListingPartial")]
        public async Task<IActionResult> ListingPartial(int categoryID)
        {
            // Custom fields
            var customFieldCategoryQuery = await _customFieldCategoryService.Query(x => x.CategoryID == categoryID)
                .Include(x => x.Include(y => y.MetaField).ThenInclude(z => z.ListingMetas)).SelectAsync();
            var customFieldCategories = customFieldCategoryQuery.ToList();
            var customFieldModel = new CustomFieldListingModel()
            {
                MetaCategories = customFieldCategories
            };

            return Ok(customFieldModel);
        }
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] SearchListingRequest model)
        {
            return Ok(await GetSearchResult(model));
        }

        private async Task<SearchListingResponse> GetSearchResult(SearchListingRequest model)
        {
            IQueryable<Listing> items = _listingService.Queryable();
            SearchListingResponse response = new SearchListingResponse();
            Region region = GetRegionFromModel(model);
            Category category = GetCategoryFromModel(model);

            if (region != null )
            {
                items = items.Where(x => x.RegionId == region.ID);
            }
            if (category != null)
            {
                items = items.Where(x => x.CategoryID == category.ID);
            }
            
            response.ListingTypes = _mapper.Map<List<SimpleListingType>>(_dataCacheService.ListingTypes);
            

            items = items
                .Include(y => y.ListingPictures)
                .Include(y => y.Category)
                .Include(y=>y.Region)
                .Include(y => y.ListingType)
                .Include(y => y.AspNetUser)
                .Include(y => y.ListingReviews);

            // Search Text
            if (!string.IsNullOrEmpty(model.SearchText))
            {
                model.SearchText = model.SearchText.ToLower();

                items = items.Where(x =>
                    x.Title.ToLower().Contains(model.SearchText) ||
                    x.Description.ToLower().Contains(model.SearchText) ||
                    x.Location.ToLower().Contains(model.SearchText));
                
            }

            // Filter items by Listing Type
            if (model.ListingTypeID != null && model.ListingTypeID.Count > 0)
            {
                items = items.Where(x => model.ListingTypeID.Contains(x.ListingTypeID));
            }
            // Location
            if (!string.IsNullOrEmpty(model.Location))
            {
                items = items.Where(x => !string.IsNullOrEmpty(x.Location) && x.Location.IndexOf(model.Location, StringComparison.OrdinalIgnoreCase) != -1);
            }

            // Picture
            if (model.PhotoOnly)
            {
                items = items.Where(x => x.ListingPictures.Count > 0);
            }
            /// Price
            if (model.PriceFrom.HasValue)
            {
                items = items.Where(x => x.Price >= model.PriceFrom.Value);
            }
            if (model.PriceTo.HasValue)
            {
                items = items.Where(x => x.Price <= model.PriceTo.Value);
            }
            // Show active and enabled only
            var itemsModelList = new List<ListingItemModel>();
            foreach (var item in (await items.ToListAsync()).Where(x => x.Active && x.Enabled).OrderByDescending(x => x.Created))
            {
                itemsModelList.Add(new ListingItemModel()
                {
                    ListingCurrent = _mapper.Map<SimpleListing>(item),
                    UrlPicture = item.ListingPictures.Count == 0 ? _imageHelper.GetListingImagePath(0) : _imageHelper.GetListingImagePath(item.ListingPictures.OrderBy(x => x.Ordering).FirstOrDefault().PictureID)
                });
            }
            var breadCrumb = GetParents(category?.ID ?? -1).Reverse().ToList();

            response.BreadCrumb = _mapper.Map<List<SimpleCategory>>(breadCrumb);
            response.Categories = _mapper.Map<List<SimpleCategory>>(_dataCacheService.Categories);

            //response.Listings = itemsModelList;
            response.ListingsPageList = itemsModelList.ToPagedList(model.PageNumber ?? 0, model.PageSize ?? 1);
            response.PagedListMetaData = response.ListingsPageList.GetMetaData();
            return response;
        }

        private Category GetCategoryFromModel(SearchListingRequest request)
        {
            var segments = request.UrlSegments;
            segments.Reverse();
            foreach (var s in segments)
            {
                var c = _dataCacheService.Categories.FirstOrDefault(cat => s.Contains(cat.Slug, StringComparison.OrdinalIgnoreCase));
                if (c != null)
                {
                    return c;
                }
            }
            return null;
        }

        private Region GetRegionFromModel(SearchListingRequest request)
        {
            var segments = request.UrlSegments;
            segments.Reverse();
            foreach(var s in segments)
            {
                if (String.IsNullOrEmpty(s))
                {
                    continue;
                }
                var r = _dataCacheService.Regions.FirstOrDefault(re => s.Contains(re.Slug, StringComparison.OrdinalIgnoreCase));
                if (r!= null)
                {
                    return r;
                }
            }
            return null;
        }

        private IEnumerable<Category> GetParents(int categoryId)
        {
            Category category = _categoryService.Find(categoryId);
            while (category != null && category.Parent != category.ID)
            {
                yield return category;
                category = _categoryService.Find(category.Parent);
            }
        }

        [HttpGet("ListingType")]
        public IActionResult ListingType(int listingTypeID)
        {
            var listingType = _dataCacheService.ListingTypes.Where(x => x.ID == listingTypeID).FirstOrDefault();

            if (listingType == null)
                return Ok();

            return Ok(new
            {
                PaymentEnabled = listingType.PaymentEnabled,
                PriceEnabled = listingType.PriceEnabled
            });
        }

        [HttpGet("Listing")]
        [AllowAnonymous]
        public async Task<IActionResult> Listing(int id)
        {
            //var itemQuery = await _listingService.Query(x => x.ID == id)
            //    .Include(x => x.Include(y => y.Category))
            //    .Include(x => x.Include(y => y.Region))
            //    .Include(x => x.Include(y => y.ListingMetas))
            //    .Include(x => x.Include(y => y.ListingMetas).ThenInclude(z => z.MetaField))
            //    .Include(x => x.Include(y => y.ListingStats))
            //    .Include(x => x.Include(y => y.ListingType))
            //    .SelectAsync();
            var itemQuery = _listingService.Queryable().AsNoTracking().Where(t => t.ID == id)
                .Include(x => x.Category).Include(x => x.Region).Include(x => x.ListingMetas).ThenInclude(z => z.MetaField).Include(x => x.ListingStats).Include(x => x.ListingType);

            var item = itemQuery.FirstOrDefault();

            if (item == null)
                return NotFound();

            var orders = _orderService.Queryable()
                .Where(x => x.ListingID == id
                    && (x.Status != (int)Enum_OrderStatus.Pending || x.Status != (int)Enum_OrderStatus.Confirmed)
                    && (x.FromDate.HasValue && x.ToDate.HasValue)
                    && (x.FromDate >= DateTime.Now || x.ToDate >= DateTime.Now))
                    .ToList();

            List<DateTime> datesBooked = new List<DateTime>();
            foreach (var order in orders)
            {
                for (DateTime date = order.FromDate.Value; date <= order.ToDate.Value; date = date.Date.AddDays(1))
                {
                    datesBooked.Add(date);
                }
            }

            var pictures = await _listingPictureservice.Query(x => x.ListingID == id).SelectAsync();

            var picturesModel = pictures.Select(x =>
                new PictureModel()
                {
                    ID = x.PictureID,
                    Url = _imageHelper.GetListingImagePath(x.PictureID),
                    ListingID = x.ListingID,
                    Ordering = x.Ordering
                }).OrderBy(x => x.Ordering).ToList();

            var reviews = await _listingReviewService
                .Query(x => x.UserTo == item.UserID)
                .Include(x => x.Include(y => y.AspNetUserFrom))
                .SelectAsync();

            var user = await _userManager.FindByIdAsync(item.UserID);

            var itemModel = new ListingItemModel()
            {
                ListingCurrent = _mapper.Map<SimpleListing>(item),
                Pictures = picturesModel,
                DatesBooked = datesBooked,
                User = _mapper.Map<SimpleUser>(user),
                ListingReviews = reviews.ToList()
            };

            // Update stat count
            //var itemStat = item.ListingStats.FirstOrDefault();
            //if (itemStat == null)
            //{
            //    _ListingStatservice.Insert(new ListingStat()
            //    {
            //        ListingID = id,
            //        CountView = 1,
            //        Created = DateTime.Now,
            //        ObjectState = ObjectState.Added
            //    });
            //}
            //else
            //{
            //    itemStat.CountView++;
            //    itemStat.ObjectState = ObjectState.Modified;
            //    _ListingStatservice.Update(itemStat);
            //}

            //await _unitOfWorkAsync.SaveChangesAsync();

            return Ok(itemModel);
        }

        [HttpPost("ListingUpdate")]
        public async Task<ActionResult> ListingUpdate(ListingUpdateModel listing)
        {
            if (_dataCacheService.Categories.Count == 0)
            {
                var resultFailed = new
                {
                    Success = false,
                    Message = "[[[There are not categories available yet.]]]"
                };
                return Ok(resultFailed);
            }

            var userIdCurrent = User.GetUserId();

            // Register account if not login
            if (!User.Identity.IsAuthenticated)
            {
                var resultFailed = new
                {
                    Success = false,
                    Message = "[[[Đăng nhập trước khi đăng tin.]]]"
                };
                return Ok(resultFailed);
            }

            bool updateCount = false;

            // Set default listing type ID
            if (listing.ListingTypeId == 0)
            {
                var listingTypes = _dataCacheService.ListingTypes;

                if (listingTypes == null)
                {
                    var resultFailed = new
                    {
                        Success = false,
                        Message = "[[[There are not listing types available yet.]]]"
                    };
                    return Ok(resultFailed);
                }

                listing.ListingTypeId = listingTypes.FirstOrDefault().ID;
            }

            if (listing.ID == 0)
            {
                //listing.ObjectState = ObjectState.Added;
                //listing.IP = Request.GetVisitorIP();
                //listing.Expiration = DateTime.MaxValue.AddDays(-1);
                //listing.UserID = userIdCurrent;
                //listing.Enabled = true;

                //updateCount = true;
                //_listingService.Insert(listing);
            }
            else
            {
                if (await NotMeListing(listing.ID))
                    return Unauthorized();

                var listingExisting = await _listingService.FindAsync(listing.ID);

                listingExisting.Title = listing.Title;
                listingExisting.Description = listing.Description;
                listingExisting.Active = listing.Active ?? true;
                listingExisting.Price = listing.Price;

                listingExisting.ContactEmail = listing.ContactEmail;
                listingExisting.ContactName = listing.ContactName;
                listingExisting.ContactPhone = listing.ContactPhone;

                listingExisting.Latitude = listing.Latitude;
                listingExisting.Longitude = listing.Longitude;
                listingExisting.Location = listing.Location;

                listingExisting.ShowPhone = listing.ShowPhone ?? true;
                listingExisting.ShowEmail = listing.ShowEmail ?? true;

                listingExisting.CategoryID = listing.CategoryIds.Last();
                listingExisting.ListingTypeID = listing.ListingTypeId;

                listingExisting.ObjectState = ObjectState.Modified;

                _listingService.Update(listingExisting);
            }

            // Delete existing fields on item
            var customFieldItemQuery = await _customFieldListingService.Query(x => x.ListingID == listing.ID).SelectAsync();
            var customFieldIds = customFieldItemQuery.Select(x => x.ID).ToList();
            foreach (var customFieldId in customFieldIds)
            {
                await _customFieldListingService.DeleteAsync(customFieldId);
            }

            // Get custom fields
            var customFieldCategoryQuery = await _customFieldCategoryService.Query(x => x.CategoryID == listing.CategoryIds.Last())
                .Include(x => x.Include(y => y.MetaField).ThenInclude(z => z.ListingMetas)).SelectAsync();
            var customFieldCategories = customFieldCategoryQuery.ToList();

            foreach (var metaCategory in customFieldCategories)
            {
                //var field = metaCategory.MetaField;
                //var controlType = (Enum_MetaFieldControlType)field.ControlTypeID;

                //string controlId = string.Format("customfield_{0}_{1}_{2}", metaCategory.ID, metaCategory.CategoryID, metaCategory.FieldID);

                //var formValue = form[controlId];

                //if (string.IsNullOrEmpty(formValue))
                //    continue;

                //formValue = formValue.ToString();

                //var itemMeta = new ListingMeta()
                //{
                //    ListingID = listing.ID,
                //    Value = formValue,
                //    FieldID = field.ID,
                //    ObjectState = ObjectState.Added
                //};

                //_customFieldListingService.Insert(itemMeta);
            }

            await _unitOfWorkAsync.SaveChangesAsync();

            //if (files.Count > 0)
            //{
            //    var itemPictureQuery = _listingPictureservice.Queryable().Where(x => x.ListingID == listing.ID);
            //    if (itemPictureQuery.Count() > 0)
            //        nextPictureOrderId = itemPictureQuery.Max(x => x.Ordering);
            //}

            //if (files != null && files.Count() > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        if ((file != null) && (file.Length > 0) && !string.IsNullOrEmpty(file.FileName))
            //        {
            //            // Picture picture and get id
            //            var picture = new Picture();
            //            picture.MimeType = "image/jpeg";
            //            _pictureService.Insert(picture);
            //            await _unitOfWorkAsync.SaveChangesAsync();

            //            Size size = new Size(500, 0);
            //            using (Image<Rgba32> image = Image.Load(file.OpenReadStream()))
            //            {
            //                var path = Path.Combine(_environment.WebRootPath, "/images/listing", string.Format("{0}.{1}", picture.ID.ToString("00000000"), "jpg"));

            //                image.Mutate(x => x
            //                     .Resize(image.Width / 2, image.Height / 2));
            //                image.Save(path); // Automatic encoder selected based on extension.
            //            }

            //            var itemPicture = new ListingPicture
            //            {
            //                ListingID = listing.ID,
            //                PictureID = picture.ID,
            //                Ordering = nextPictureOrderId
            //            };

            //            _listingPictureservice.Insert(itemPicture);

            //            nextPictureOrderId++;
            //        }
            //    }
            //}

            await _unitOfWorkAsync.SaveChangesAsync();

            // Update statistics count
            if (updateCount)
            {
                _sqlDbService.UpdateCategoryItemCount(listing.CategoryIds.Last());
                _dataCacheService.RemoveCachedItem(CacheKeys.Statistics);
            }
            var result = new { Success = true, Message = "[[[Listing is updated!]]]" };
            return Ok(result);
        }

        [HttpPost("ListingDelete")]
        public async Task<ActionResult> ListingDelete(int id)
        {
            var item = await _listingService.FindAsync(id);
            var orderQuery = await _orderService.Query(x => x.ListingID == id).SelectAsync();

            // Delete item if no orders associated with it
            if (item.Orders.Count > 0)
            {
                var resultFailed = new { Success = false, Message = "[[[You cannot delete item with orders! You can deactivate it instead.]]]" };
                return Ok(resultFailed);
            }

            await _listingService.DeleteAsync(id);

            await _unitOfWorkAsync.SaveChangesAsync();

            var result = new { Success = true, Message = "[[[Your listing has been deleted.]]]" };
            return Ok(result);
        }
        [HttpPost("ListingPhotoDelete")]
        public async Task<IActionResult> ListingPhotoDelete(int id)
        {
            try
            {
                await _pictureService.DeleteAsync(id);
                var itemPicture = _listingPictureservice.Query(x => x.PictureID == id).Select().FirstOrDefault();

                if (itemPicture != null)
                    await _listingPictureservice.DeleteAsync(itemPicture.ID);

                await _unitOfWorkAsync.SaveChangesAsync();

                var path = Path.Combine(_environment.ContentRootPath, "/images/listing", string.Format("{0}.{1}", id.ToString("00000000"), "jpg"));

                System.IO.File.Delete(path);

                var result = new { Success = "true", Message = "" };
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new { Success = "false", Message = ex.Message };
                return Ok(result);
            }
        }

        [AllowAnonymous]
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var items = await _listingService.Query(x => x.UserID == id)
                .Include(x => x.Include(y => y.ListingPictures))
                .Include(x => x.Include(y => y.ListingType))
                .Include(x => x.Include(y => y.AspNetUser))
                .Include(x => x.Include(y => y.ListingReviews))
                .SelectAsync();

            var itemsModel = new List<ListingItemModel>();
            foreach (var item in items.OrderByDescending(x => x.Created))
            {
                itemsModel.Add(new ListingItemModel()
                {
                    ListingCurrent =_mapper.Map<SimpleListing>(item),
                    UrlPicture = item.ListingPictures.Count == 0 ? _imageHelper.GetListingImagePath(0) : _imageHelper.GetListingImagePath(item.ListingPictures.OrderBy(x => x.Ordering).FirstOrDefault().PictureID)
                });
            }

            // include reviews
            var reviews = await _listingReviewService
                .Query(x => x.UserTo == id)
                .Include(x => x.Include(y => y.AspNetUserFrom))
                .SelectAsync();

            var model = new ProfileModel()
            {
                Listings = itemsModel,
                User = user,
                ListingReviews = reviews.ToList()
            };

            return Ok(model);
        }

        private async Task<bool> NotMeListing(int id)
        {
            var userId = User.GetUserId();
            var item = await _listingService.FindAsync(id);
            return item.UserID != userId;
        }

        #endregion
    }
}
