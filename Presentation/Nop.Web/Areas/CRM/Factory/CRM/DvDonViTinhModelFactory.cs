//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 21/9/2020
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
    public class DvDonViTinhModelFactory : IDvDonViTinhModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDvDonViTinhService _itemService;

        #endregion

        #region Ctor

        public DvDonViTinhModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IDvDonViTinhService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region DvDonViTinh
        public DvDonViTinhSearchModel PrepareDvDonViTinhSearchModel(DvDonViTinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DvDonViTinhListModel PrepareDvDonViTinhListModel(DvDonViTinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDvDonViTinhs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DvDonViTinhListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<DvDonViTinhModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<DvDonViTinhModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public DvDonViTinhModel PrepareDvDonViTinhModel(DvDonViTinhModel model, DvDonViTinh item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DvDonViTinhModel>();
            }
            //more

            return model;
        }
        public void PrepareDvDonViTinh(DvDonViTinhModel model, DvDonViTinh item)
        {
            item.TEN = model.TEN;
        }

        public IList<SelectListItem> PrepareSelectListDonViTinh(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị tính --", string valueFirstRow = "")
        {
            var _listItems = _itemService.GetAllDvDonViTinhs();
            var selectList = _listItems.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.Id.ToString(),
                Selected = valSelected == c.Id
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

