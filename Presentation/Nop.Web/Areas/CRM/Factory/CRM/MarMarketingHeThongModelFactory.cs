//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 19/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
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
using System.Linq;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class MarMarketingHeThongModelFactory : IMarMarketingHeThongModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMarMarketingHeThongService _itemService;

        #endregion

        #region Ctor

        public MarMarketingHeThongModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IMarMarketingHeThongService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region MarMarketingHeThong
        public MarMarketingHeThongSearchModel PrepareMarMarketingHeThongSearchModel(MarMarketingHeThongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public MarMarketingHeThongListModel PrepareMarMarketingHeThongListModel(MarMarketingHeThongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarMarketingHeThongs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new MarMarketingHeThongListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarMarketingHeThongModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<MarMarketingHeThongModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public MarMarketingHeThongModel PrepareMarMarketingHeThongModel(MarMarketingHeThongModel model, MarMarketingHeThong item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MarMarketingHeThongModel>();
            }
            //more

            return model;
        }
        public void PrepareMarMarketingHeThong(MarMarketingHeThongModel model, MarMarketingHeThong item)
        {
            item.TEN = model.TEN;
            item.MA = model.MA;
        }
        public IList<SelectListItem> PrepareMultiSelectMarHethong(IList<int> valSelected = null, bool isAddFirst = false, string strFirstRow = "-- Chọn marketing hệ thống --", string valueFirstRow = "")
        {
            var _listItems = _itemService.GetAllMarMarketingHeThongs();
            var selectList = _listItems.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.Id.ToString(),
                Selected = valSelected != null && valSelected.Contains(c.Id)
            }).ToList();
            if (isAddFirst)
                selectList.Add(new SelectListItem
                {
                    Text = strFirstRow,
                    Value = valueFirstRow,
                });

            return selectList.OrderBy(c => c.Value).ToList();
        }
        #endregion
    }
}

