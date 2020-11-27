//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
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
    public class DvLoaiDichVuModelFactory : IDvLoaiDichVuModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDvLoaiDichVuService _itemService;

        #endregion

        #region Ctor

        public DvLoaiDichVuModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IDvLoaiDichVuService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region DvLoaiDichVu
        public DvLoaiDichVuSearchModel PrepareDvLoaiDichVuSearchModel(DvLoaiDichVuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DvLoaiDichVuListModel PrepareDvLoaiDichVuListModel(DvLoaiDichVuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDvLoaiDichVus(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DvLoaiDichVuListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<DvLoaiDichVuModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<DvLoaiDichVuModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public DvLoaiDichVuModel PrepareDvLoaiDichVuModel(DvLoaiDichVuModel model, DvLoaiDichVu item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DvLoaiDichVuModel>();
            }
            //more

            return model;
        }
        public void PrepareDvLoaiDichVu(DvLoaiDichVuModel model, DvLoaiDichVu item)
        {
            item.TEN = model.TEN;
        }
        public IList<SelectListItem> PrepareSelectListLoaiDichVu(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn loại dịch vụ --", string valueFirstRow = "")
        {
            var _listItems = _itemService.GetAllDvLoaiDichVus();
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
                    Selected = valSelected == 0
                });

            return selectList.OrderBy(c => c.Value).ToList();
        }
        public bool CheckTenExist(string ten)
        {
            if (_itemService.CountLoaiDichVu(ten) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

