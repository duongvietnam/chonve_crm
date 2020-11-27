//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;
using Nop.Services.HeThong;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Core.Domain.HeThong;
using Nop.Web.Models.Media;
using Nop.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IO;
using Nop.Services.Media;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Core.Configuration;

namespace Nop.Web.Controllers
{
    public partial class WorkfilesController : BaseController
    {
        const string KEY_CACHE_ITEM = "FILE_CACHE_ITEM_{0}";
        #region Fields
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly INotificationService _notificationService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkfilesService _itemService;
        private readonly INopFileProvider _fileProvider;
        private readonly NopConfig _nopConfig;
        #endregion

        #region Ctor
        public WorkfilesController(
            NopConfig nopConfig,
            IStaticCacheManager staticCacheManager,
            INopFileProvider fileProvider,
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IWorkfilesService itemService
            )
        {
            this._staticCacheManager = staticCacheManager;
            this._nopConfig = nopConfig;
            this._fileProvider = fileProvider;
            this._storeContext = storeContext;
            this._notificationService = notificationService;
            this._customerActivityService = customerActivityService;
            this._eventPublisher = eventPublisher;
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._workContext = workContext;
            this._localizationSettings = localizationSettings;
            this._itemService = itemService;
        }
        #endregion
        #region Methods

        void SaveWorkFileOnDisk(WorkFiles item, byte[] fileContent)
        {
            string _pathStore = item.NgayTao.ToPathFolderStore();
            _pathStore = _fileProvider.Combine(_fileProvider.MapPath(_nopConfig.FolderWorkFiles), _pathStore);
            _fileProvider.CreateDirectory(_pathStore);
            var _fileStore = _fileProvider.Combine(_pathStore, item.Guid.ToString() + item.DuoiFile);
            _fileProvider.WriteAllBytes(_fileStore, fileContent);
        }
        byte[] GetWorkFileContentOnDisk(WorkfilesModel item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(_nopConfig.FolderWorkFiles), item.NgayTao.ToPathFolderStore(), item.Guid.ToString() + item.DuoiFile);
            return _fileProvider.ReadAllBytes(_fileStore);
        }

        
        public virtual IActionResult Download(string guid)
        {
            var item = _itemService.GetFileByGuid(new Guid(guid));
            var fmodel= item != null ? item.ToModel<WorkfilesModel>() : new WorkfilesModel();
           
            var contentFile = GetWorkFileContentOnDisk(fmodel);
            if (contentFile == null)
                return Content("File not found with the specified guid");
            var fileName = !string.IsNullOrWhiteSpace(fmodel.Ten) ? fmodel.Ten : fmodel.Id.ToString();
            var contentType = !string.IsNullOrEmpty(fmodel.LoaiNoiDung)
                ? fmodel.LoaiNoiDung
                : MimeTypes.ApplicationOctetStream;
            return new FileContentResult(contentFile, contentType)
            {
                FileDownloadName = fileName + fmodel.DuoiFile
            };
        }
        private IActionResult ViewNoAvatar()
        {
            var pathNoAvatar = _fileProvider.GetAbsolutePath("images", NopMediaDefaults.DefaultAvatarFileName);
            var contentFile = _fileProvider.ReadAllBytes(pathNoAvatar);
            return new FileContentResult(contentFile, "image/jpeg");
        }
        public virtual IActionResult ViewImage(int Id)
        {
            if (Id == 0)
            {
                return ViewNoAvatar();
            }
            var item = _itemService.GetWorkfilesById(Id);
            var fmodel = item != null ? item.ToModel<WorkfilesModel>() : new WorkfilesModel();
           
            if (fmodel.Id == 0) return ViewNoAvatar();

            var contentFile = GetWorkFileContentOnDisk(fmodel);
            if (contentFile == null || contentFile.Length == 0)
                return Content("File not found with the specified guid");
            var fileName = !string.IsNullOrWhiteSpace(fmodel.Ten) ? fmodel.Ten : fmodel.Id.ToString();
            var contentType = !string.IsNullOrEmpty(fmodel.LoaiNoiDung)
                ? fmodel.LoaiNoiDung
                : MimeTypes.ApplicationOctetStream;
            return new FileContentResult(contentFile, contentType)
            {
                FileDownloadName = fileName + fmodel.DuoiFile
            };
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                var item = _itemService.GetWorkfilesById(Id);
                _itemService.DeleteWorkfiles(item);
            }
            catch (Exception ex)
            {
                return JsonErrorMessage(ex.Message);
            }

            return JsonSuccessMessage();
        }
        [HttpPost]
        public ActionResult SaveDropzoneJsUploadedFiles()
        {
            List<WorkfilesModel> ls = new List<WorkfilesModel>();
            foreach (var file in Request.Form.Files)
            {
                string fname = file.FileName;

                //You can Save the file content here
                var item = UploadFile(file);
                if (item != null)
                {
                    var model = item.ToModel<WorkfilesModel>();
                    ls.Add(model);
                }
            }
            return Json(ls);

        }
        private byte[] GetWorkFileBits(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
        }
        private WorkFiles UploadFile(IFormFile httpPostedFile, bool isImage = false)
        {
            if (httpPostedFile == null)
            {
                return null;
            }

            var fileBinary = GetWorkFileBits(httpPostedFile);

            var qqFileNameParameter = "qqfilename";
            var fileName = httpPostedFile.FileName;
            if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                fileName = Request.Form[qqFileNameParameter].ToString();
            //remove path (passed in IE)
            fileName = _fileProvider.GetFileName(fileName);

            var contentType = httpPostedFile.ContentType;
            if (isImage)
            {
                var mimeType = CommonHelper.EnsureNotNull(contentType);
                mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);
                fileBinary = _pictureService.ValidatePicture(fileBinary, mimeType);
            }

            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            var fwork = new WorkFiles
            {
                Guid = Guid.NewGuid(),
                LoaiNoiDung = contentType,
                //we store filename without extension for downloads
                Ten = _fileProvider.GetFileNameWithoutExtension(fileName),
                DuoiFile = fileExtension,
                NgayTao = DateTime.Now,
                CustomerId = _workContext.CurrentCustomer.Id,
                KichThuoc = Convert.ToDecimal(fileBinary.LongLength / 1024) //luu thanh kb
            };
            _itemService.InsertWorkfiles(fwork);
            //luu file content ra ngoai
            SaveWorkFileOnDisk(fwork, fileBinary);
            return fwork;
        }
        [HttpPost]
        //do not validate request token (XSRF)
        public virtual IActionResult AsyncUpload()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded",
                    downloadGuid = Guid.Empty
                });
            }
            var fwork = UploadFile(httpPostedFile);
            return Json(fwork.ToModel<WorkfilesModel>());
        }

        [HttpPost]
        public virtual IActionResult UploadAvatar(IFormFile fileAvatar)
        {
            var fwork = UploadFile(fileAvatar, true);
            return JsonSuccessMessage("Ok", fwork.ToModel<WorkfilesModel>());
        }


        #endregion
    }
}

