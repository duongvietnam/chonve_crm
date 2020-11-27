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
    public class MarEventMarketingModelFactory : IMarEventMarketingModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMarEventMarketingService _itemService;
        private readonly IMarMarketingModelFactory _marMarketingModelFactory;
        private readonly IMarMarketingService _marMarketingService;
        #endregion

        #region Ctor

        public MarEventMarketingModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IMarEventMarketingService itemService,
            IMarMarketingModelFactory marMarketingModelFactory,
            IMarMarketingService marMarketingService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._marMarketingModelFactory = marMarketingModelFactory;
            this._marMarketingService = marMarketingService;
        }
        #endregion

        #region MarEventMarketing
        public MarEventMarketingSearchModel PrepareMarEventMarketingSearchModel(MarEventMarketingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public MarEventMarketingListModel PrepareMarEventMarketingListModel(MarEventMarketingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarEventMarketings(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new MarEventMarketingListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarEventMarketingModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<MarEventMarketingModel>();
                    itemModel.TenMarketing = _marMarketingService.GetMarMarketingById((int)item.MARKETING_ID).TEN;
                    itemModel.BatDau = item.THOI_GIAN_BAT_DAU.toDateVNString();
                    itemModel.KetThuc = item.THOI_GIAN_KET_THUC.toDateVNString();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public MarEventMarketingModel PrepareMarEventMarketingModel(MarEventMarketingModel model, MarEventMarketing item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MarEventMarketingModel>();
            }
            //more
            model.DDLMarketing = _marMarketingModelFactory.PrepareSelectListMarketing(isAddFirst: true);

            return model;
        }
        public void PrepareMarEventMarketing(MarEventMarketingModel model, MarEventMarketing item)
        {
            item.MARKETING_ID = model.MARKETING_ID;
            item.DOANH_NGHIEP_ID = model.DOANH_NGHIEP_ID;
            item.THOI_GIAN_BAT_DAU = model.THOI_GIAN_BAT_DAU;
            item.THOI_GIAN_KET_THUC = model.THOI_GIAN_KET_THUC;
            item.TEN = model.TEN;
        }
        #endregion
    }
}

