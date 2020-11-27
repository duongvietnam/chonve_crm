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
    public class KhNhomHeThongMapModelFactory:IKhNhomHeThongMapModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKhNhomHeThongMapService _itemService;
            
         #endregion
         
         #region Ctor

        public KhNhomHeThongMapModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IKhNhomHeThongMapService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region KhNhomHeThongMap
        public KhNhomHeThongMapSearchModel PrepareKhNhomHeThongMapSearchModel(KhNhomHeThongMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public KhNhomHeThongMapListModel PrepareKhNhomHeThongMapListModel(KhNhomHeThongMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhNhomHeThongMaps(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new KhNhomHeThongMapListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhNhomHeThongMapModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<KhNhomHeThongMapModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public KhNhomHeThongMapModel PrepareKhNhomHeThongMapModel(KhNhomHeThongMapModel model, KhNhomHeThongMap item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhNhomHeThongMapModel>();
            }
            //more
           
            return model;
        }
       public void PrepareKhNhomHeThongMap(KhNhomHeThongMapModel model, KhNhomHeThongMap item)
        {
		item.NHOM_KHACH_HANG_ID = model.NHOM_KHACH_HANG_ID;
		item.NHOM_KHACH_HANG_HE_THONG = model.NHOM_KHACH_HANG_HE_THONG;
            
        }
        #endregion	
     }
}		

