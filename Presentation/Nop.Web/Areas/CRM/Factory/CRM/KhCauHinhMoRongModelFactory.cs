//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.CRM;
using Nop.Services;
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
    public class KhCauHinhMoRongModelFactory : IKhCauHinhMoRongModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IKhCauHinhMoRongService _itemService;

        #endregion

        #region Ctor

        public KhCauHinhMoRongModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IKhCauHinhMoRongService itemService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region KhCauHinhMoRong
        public KhCauHinhMoRongSearchModel PrepareKhCauHinhMoRongSearchModel(KhCauHinhMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public KhCauHinhMoRongListModel PrepareKhCauHinhMoRongListModel(KhCauHinhMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhCauHinhMoRongs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new KhCauHinhMoRongListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhCauHinhMoRongModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhCauHinhMoRongModel>();
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public KhCauHinhMoRongModel PrepareKhCauHinhMoRongModel(KhCauHinhMoRongModel model, KhCauHinhMoRong item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhCauHinhMoRongModel>();
            }
            //more
            model.DLLLoaiDuLieu = new LoaiDuLieuEnum().ToSelectList();
            model.DDLCauHinhCha = PrepareSelectListCauHinhMoRong(isAddFirst: true);

            return model;
        }
        public void PrepareKhCauHinhMoRong(KhCauHinhMoRongModel model, KhCauHinhMoRong item)
        {
            item.PARENT_ID = model.PARENT_ID;
            item.TEN = model.TEN;
            item.LOAI_DU_LIEU = model.LOAI_DU_LIEU;
        }
        public IList<SelectListItem> PrepareSelectListCauHinhMoRong(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn cấu hình --", string valueFirstRow = "")
        {
            var _listItems = _itemService.GetAllKhCauHinhMoRongs();
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
            if (_itemService.GetCountCauHinhMoRong(ten) > 0)
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

