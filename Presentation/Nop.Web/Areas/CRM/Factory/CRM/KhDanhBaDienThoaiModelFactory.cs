//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;
using Nop.Core.Domain.CRM;
using Nop.Services.CRM;
using Nop.Web.Areas.CRM.Models.CRM;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class KhDanhBaDienThoaiModelFactory : IKhDanhBaDienThoaiModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IKhDanhBaDienThoaiService _itemService;

        #endregion

        #region Ctor

        public KhDanhBaDienThoaiModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IKhDanhBaDienThoaiService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region KhDanhBaDienThoai
        public KhDanhBaDienThoaiSearchModel PrepareKhDanhBaDienThoaiSearchModel(KhDanhBaDienThoaiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public KhDanhBaDienThoaiListModel PrepareKhDanhBaDienThoaiListModel(KhDanhBaDienThoaiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhDanhBaDienThoais(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new KhDanhBaDienThoaiListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhDanhBaDienThoaiModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhDanhBaDienThoaiModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public KhDanhBaDienThoaiModel PrepareKhDanhBaDienThoaiModel(KhDanhBaDienThoaiModel model, KhDanhBaDienThoai item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhDanhBaDienThoaiModel>();
            }
            //more

            return model;
        }
        public void PrepareKhDanhBaDienThoai(KhDanhBaDienThoaiModel model, KhDanhBaDienThoai item)
        {
            item.SO_DIEN_THOAI = model.SO_DIEN_THOAI;
            item.NHOM_DANH_BA = model.NHOM_DANH_BA;
        }
        #endregion
    }
}

