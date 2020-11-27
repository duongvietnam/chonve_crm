//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.CRM;
using Nop.Services.CRM;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class MarDoanhThuMarketingModelFactory : IMarDoanhThuMarketingModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMarDoanhThuMarketingService _itemService;

        #endregion

        #region Ctor

        public MarDoanhThuMarketingModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IMarDoanhThuMarketingService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region MarDoanhThuMarketing
        public MarDoanhThuMarketingSearchModel PrepareMarDoanhThuMarketingSearchModel(MarDoanhThuMarketingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public MarDoanhThuMarketingListModel PrepareMarDoanhThuMarketingListModel(MarDoanhThuMarketingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarDoanhThuMarketings(MarEventId: searchModel.MarEventId, StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new MarDoanhThuMarketingListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarDoanhThuMarketingModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<MarDoanhThuMarketingModel>();
                    itemModel.NgaySuKien = item.NGAY_EVENT.toDateVNString();
                    itemModel.DoanhThuString = item.DOANH_THU.ToVNStringPrice();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public MarDoanhThuMarketingModel PrepareMarDoanhThuMarketingModel(MarDoanhThuMarketingModel model, MarDoanhThuMarketing item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MarDoanhThuMarketingModel>();
            }
            //more

            return model;
        }
        public void PrepareMarDoanhThuMarketing(MarDoanhThuMarketingModel model, MarDoanhThuMarketing item)
        {
            item.EVENT_ID = model.EVENT_ID;
            item.NGAY_EVENT = model.NGAY_EVENT;
            item.LUOT_KHACH = model.LUOT_KHACH;
            item.DOANH_THU = model.DOANH_THU;
            item.DOANH_NGHIEP_ID = model.DOANH_NGHIEP_ID;

        }
        #endregion
    }
}

