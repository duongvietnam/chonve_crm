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
    public class GdGiaoDichDanhGiaModelFactory : IGdGiaoDichDanhGiaModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IGdGiaoDichDanhGiaService _itemService;

        #endregion

        #region Ctor

        public GdGiaoDichDanhGiaModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IGdGiaoDichDanhGiaService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region GdGiaoDichDanhGia
        public GdGiaoDichDanhGiaSearchModel PrepareGdGiaoDichDanhGiaSearchModel(GdGiaoDichDanhGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public GdGiaoDichDanhGiaListModel PrepareGdGiaoDichDanhGiaListModel(GdGiaoDichDanhGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchGdGiaoDichDanhGias(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new GdGiaoDichDanhGiaListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<GdGiaoDichDanhGiaModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<GdGiaoDichDanhGiaModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public GdGiaoDichDanhGiaModel PrepareGdGiaoDichDanhGiaModel(GdGiaoDichDanhGiaModel model, GdGiaoDichDanhGia item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<GdGiaoDichDanhGiaModel>();
            }
            //more

            return model;
        }
        public void PrepareGdGiaoDichDanhGia(GdGiaoDichDanhGiaModel model, GdGiaoDichDanhGia item)
        {
            item.GIAO_DICH_ID = model.GIAO_DICH_ID;
            item.SO_DIEM = model.SO_DIEM;
            item.HANG_MUC = model.HANG_MUC;
            item.DOANH_NGHIEP_ID = model.DOANH_NGHIEP_ID;
            item.GHI_CHU = model.GHI_CHU;
            item.KHACH_HANG_ID = model.KHACH_HANG_ID;
        }
        #endregion
    }
}

