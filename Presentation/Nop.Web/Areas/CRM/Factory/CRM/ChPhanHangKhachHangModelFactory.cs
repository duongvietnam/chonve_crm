//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
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
    public class ChPhanHangKhachHangModelFactory : IChPhanHangKhachHangModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IChPhanHangKhachHangService _itemService;

        #endregion

        #region Ctor

        public ChPhanHangKhachHangModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IChPhanHangKhachHangService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region ChPhanHangKhachHang
        public ChPhanHangKhachHangSearchModel PrepareChPhanHangKhachHangSearchModel(ChPhanHangKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public ChPhanHangKhachHangListModel PrepareChPhanHangKhachHangListModel(ChPhanHangKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchChPhanHangKhachHangs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ChPhanHangKhachHangListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<ChPhanHangKhachHangModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<ChPhanHangKhachHangModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public ChPhanHangKhachHangModel PrepareChPhanHangKhachHangModel(ChPhanHangKhachHangModel model, ChPhanHangKhachHang item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ChPhanHangKhachHangModel>();
            }
            //more

            return model;
        }
        public void PrepareChPhanHangKhachHang(ChPhanHangKhachHangModel model, ChPhanHangKhachHang item)
        {
            item.GIA_TRI_CAP_1 = model.GIA_TRI_CAP_1;
            item.GIA_TRI_CAP_2 = model.GIA_TRI_CAP_2;
            item.GIA_TRI_CAP_3 = model.GIA_TRI_CAP_3;
            item.TEN_CAP_1 = model.TEN_CAP_1;
            item.TEN_CAP_2 = model.TEN_CAP_2;
            item.TEN_CAP_3 = model.TEN_CAP_3;
        }
        public IList<SelectListItem> PrepareDDLPhanHangKhachHang(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn hạnh khách hàng --", string valueFirstRow = "")
        {
            var _listItems = _itemService.GetAllChPhanHangKhachHangs();
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
        public IList<SelectListItem> PrepareMultiSelectPhanHangKhachHang(IList<int> valSelected)
        {
            var _listItems = _itemService.GetAllChPhanHangKhachHangs();
            var selectList = _listItems.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.Id.ToString(),
                Selected = valSelected.Contains(c.Id)
            }).ToList();

            return selectList.OrderBy(c => c.Value).ToList();
        }
        #endregion
    }
}

