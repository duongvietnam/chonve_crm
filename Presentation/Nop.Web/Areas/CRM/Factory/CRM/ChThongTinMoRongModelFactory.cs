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
    public class ChThongTinMoRongModelFactory : IChThongTinMoRongModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IChThongTinMoRongService _itemService;

        #endregion

        #region Ctor

        public ChThongTinMoRongModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IChThongTinMoRongService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region ChThongTinMoRong
        public ChThongTinMoRongSearchModel PrepareChThongTinMoRongSearchModel(ChThongTinMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public ChThongTinMoRongListModel PrepareChThongTinMoRongListModel(ChThongTinMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchChThongTinMoRongs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ChThongTinMoRongListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<ChThongTinMoRongModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<ChThongTinMoRongModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public ChThongTinMoRongModel PrepareChThongTinMoRongModel(ChThongTinMoRongModel model, ChThongTinMoRong item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ChThongTinMoRongModel>();
            }
            //more

            return model;
        }
        public void PrepareChThongTinMoRong(ChThongTinMoRongModel model, ChThongTinMoRong item)
        {
            item.TEN = model.TEN;
            item.MA = model.MA;
            item.DOANH_NGHIEP_ID = model.DOANH_NGHIEP_ID;
            item.MO_TA = model.MO_TA;
            item.KIEU_DU_LIEU = model.KIEU_DU_LIEU;
        }
        #endregion
    }
}

