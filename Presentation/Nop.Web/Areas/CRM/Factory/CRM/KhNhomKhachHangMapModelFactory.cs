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
    public class KhNhomKhachHangMapModelFactory:IKhNhomKhachHangMapModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKhNhomKhachHangMapService _itemService;
            
         #endregion
         
         #region Ctor

        public KhNhomKhachHangMapModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IKhNhomKhachHangMapService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region KhNhomKhachHangMap
        public KhNhomKhachHangMapSearchModel PrepareKhNhomKhachHangMapSearchModel(KhNhomKhachHangMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public KhNhomKhachHangMapListModel PrepareKhNhomKhachHangMapListModel(KhNhomKhachHangMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhNhomKhachHangMaps(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new KhNhomKhachHangMapListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhNhomKhachHangMapModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<KhNhomKhachHangMapModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public KhNhomKhachHangMapModel PrepareKhNhomKhachHangMapModel(KhNhomKhachHangMapModel model, KhNhomKhachHangMap item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhNhomKhachHangMapModel>();
            }
            //more
           
            return model;
        }
       public void PrepareKhNhomKhachHangMap(KhNhomKhachHangMapModel model, KhNhomKhachHangMap item)
        {
		item.KHACH_HANG_ID = model.KHACH_HANG_ID;
		item.NHOM_KHACH_HANG_ID = model.NHOM_KHACH_HANG_ID;
		item.DIEM_DANH_GIA = model.DIEM_DANH_GIA;
            
        }
        #endregion	
     }
}		

