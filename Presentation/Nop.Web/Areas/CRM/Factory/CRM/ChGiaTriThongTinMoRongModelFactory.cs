//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
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
    public class ChGiaTriThongTinMoRongModelFactory : IChGiaTriThongTinMoRongModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IChGiaTriThongTinMoRongService _itemService;

        #endregion

        #region Ctor

        public ChGiaTriThongTinMoRongModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IChGiaTriThongTinMoRongService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region ChGiaTriThongTinMoRong
        public ChGiaTriThongTinMoRongSearchModel PrepareChGiaTriThongTinMoRongSearchModel(ChGiaTriThongTinMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public ChGiaTriThongTinMoRongListModel PrepareChGiaTriThongTinMoRongListModel(ChGiaTriThongTinMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchChGiaTriThongTinMoRongs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ChGiaTriThongTinMoRongListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<ChGiaTriThongTinMoRongModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<ChGiaTriThongTinMoRongModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public ChGiaTriThongTinMoRongModel PrepareChGiaTriThongTinMoRongModel(ChGiaTriThongTinMoRongModel model, ChGiaTriThongTinMoRong item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ChGiaTriThongTinMoRongModel>();
            }
            //more

            return model;
        }
        public void PrepareChGiaTriThongTinMoRong(ChGiaTriThongTinMoRongModel model, ChGiaTriThongTinMoRong item)
        {
            item.THONG_TIN_MO_RONG_ID = model.THONG_TIN_MO_RONG_ID;
            item.DOI_TUONG_ID = model.DOI_TUONG_ID;
            item.LOAI_DOI_TUONG = model.LOAI_DOI_TUONG;
            item.GIA_TRI = model.GIA_TRI;
        }
        #endregion
    }
}

