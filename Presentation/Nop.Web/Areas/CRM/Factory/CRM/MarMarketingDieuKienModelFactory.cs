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
    public class MarMarketingDieuKienModelFactory : IMarMarketingDieuKienModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMarMarketingDieuKienService _itemService;

        #endregion

        #region Ctor

        public MarMarketingDieuKienModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IMarMarketingDieuKienService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region MarMarketingDieuKien
        public MarMarketingDieuKienSearchModel PrepareMarMarketingDieuKienSearchModel(MarMarketingDieuKienSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public MarMarketingDieuKienListModel PrepareMarMarketingDieuKienListModel(MarMarketingDieuKienSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarMarketingDieuKiens(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new MarMarketingDieuKienListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarMarketingDieuKienModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<MarMarketingDieuKienModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public MarMarketingDieuKienModel PrepareMarMarketingDieuKienModel(MarMarketingDieuKienModel model, MarMarketingDieuKien item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MarMarketingDieuKienModel>();
            }
            //more

            return model;
        }
        public void PrepareMarMarketingDieuKien(MarMarketingDieuKienModel model, MarMarketingDieuKien item)
        {
            item.MARKETING_ID = model.MARKETING_ID;
            item.DICH_VU_ID = model.DICH_VU_ID;
            item.TU_NGAY = model.TU_NGAY;
            item.DEN_NGAY = model.DEN_NGAY;
            item.TU_GIO = model.TU_GIO;
            item.DEN_GIO = model.DEN_GIO;
            item.SO_LUONG = model.SO_LUONG;
            item.DON_VI_TINH = model.DON_VI_TINH;
            item.SALE = model.SALE;
            item.DON_GIA = model.DON_GIA;
            item.DICH_VU_JSON = model.DICH_VU_JSON;
        }
        #endregion
    }
}

