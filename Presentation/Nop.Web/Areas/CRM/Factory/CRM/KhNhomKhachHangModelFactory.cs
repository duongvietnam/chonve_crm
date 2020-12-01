//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;
using Nop.Core.Domain.CRM;
using Nop.Services.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class KhNhomKhachHangModelFactory : IKhNhomKhachHangModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IKhNhomKhachHangService _itemService;
        private readonly IKhNhomHeThongModelFactory _khNhomHeThongModelFactory;
        private readonly IKhNhomHeThongMapService _khNhomHeThongMapService;
        #endregion

        #region Ctor

        public KhNhomKhachHangModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IKhNhomKhachHangService itemService,
            IKhNhomHeThongModelFactory khNhomHeThongModelFactory,
            IKhNhomHeThongMapService khNhomHeThongMapService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._khNhomHeThongModelFactory = khNhomHeThongModelFactory;
            this._khNhomHeThongMapService = khNhomHeThongMapService;
        }
        #endregion

        #region KhNhomKhachHang
        public KhNhomKhachHangSearchModel PrepareKhNhomKhachHangSearchModel(KhNhomKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public KhNhomKhachHangListModel PrepareKhNhomKhachHangListModel(KhNhomKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhNhomKhachHangs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new KhNhomKhachHangListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhNhomKhachHangModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhNhomKhachHangModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public KhNhomKhachHangModel PrepareKhNhomKhachHangModel(KhNhomKhachHangModel model, KhNhomKhachHang item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhNhomKhachHangModel>();
                model.DDLNhomHeThong = _khNhomHeThongModelFactory.PrepareSelectListNhomHeThong(valSelected: _khNhomHeThongMapService.GetKhNhomHeThongByNhomId(item.Id));
            }
            else
            {
                model.DDLNhomHeThong = _khNhomHeThongModelFactory.PrepareSelectListNhomHeThong();
            }
            //more

            return model;
        }
        public void PrepareKhNhomKhachHang(KhNhomKhachHangModel model, KhNhomKhachHang item)
        {
            item.TEN = model.TEN;
        }
        public bool CheckTenExist(string ten)
        {
            if (_itemService.GetCountNhomKhachHang(ten) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<SelectListItem> PrepareSelectListNhomKhachHang(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nhóm khách hàng --", string valueFirstRow = "", int storeId = 0)
        {
            var _listItems = _itemService.GetAllKhNhomKhachHangs(storeId);
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

        public IList<SelectListItem> PrepareMultiSelectListNhomKhachHang(IList<int> valSelected, int storeId = 0)
        {
            var _listItems = _itemService.GetAllKhNhomKhachHangs(storeId);
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
