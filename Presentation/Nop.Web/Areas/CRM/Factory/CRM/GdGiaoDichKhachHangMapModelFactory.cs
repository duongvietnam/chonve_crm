//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;
using Nop.Core.Domain.CRM;
using Nop.Services.CRM;
using Nop.Web.Areas.CRM.Models.CRM;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class GdGiaoDichKhachHangMapModelFactory:IGdGiaoDichKhachHangMapModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IGdGiaoDichKhachHangMapService _itemService;
            
         #endregion
         
         #region Ctor

        public GdGiaoDichKhachHangMapModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IGdGiaoDichKhachHangMapService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region GdGiaoDichKhachHangMap
        public GdGiaoDichKhachHangMapSearchModel PrepareGdGiaoDichKhachHangMapSearchModel(GdGiaoDichKhachHangMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public GdGiaoDichKhachHangMapListModel PrepareGdGiaoDichKhachHangMapListModel(GdGiaoDichKhachHangMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchGdGiaoDichKhachHangMaps(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new GdGiaoDichKhachHangMapListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<GdGiaoDichKhachHangMapModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<GdGiaoDichKhachHangMapModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public GdGiaoDichKhachHangMapModel PrepareGdGiaoDichKhachHangMapModel(GdGiaoDichKhachHangMapModel model, GdGiaoDichKhachHangMap item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<GdGiaoDichKhachHangMapModel>();
            }
            //more
           
            return model;
        }
       public void PrepareGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMapModel model, GdGiaoDichKhachHangMap item)
        {
		item.KHACH_HANG_ID = model.KHACH_HANG_ID;
		item.GIAO_DICH_ID = model.GIAO_DICH_ID;
		item.DICH_VU_ID = model.DICH_VU_ID;
		item.KHACH_HANG_CHINH = model.KHACH_HANG_CHINH;
            
        }
        #endregion	
     }
}		

