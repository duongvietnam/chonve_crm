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
    public class GdGiaoDichDiemModelFactory:IGdGiaoDichDiemModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IGdGiaoDichDiemService _itemService;
            
         #endregion
         
         #region Ctor

        public GdGiaoDichDiemModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IGdGiaoDichDiemService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region GdGiaoDichDiem
        public GdGiaoDichDiemSearchModel PrepareGdGiaoDichDiemSearchModel(GdGiaoDichDiemSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public GdGiaoDichDiemListModel PrepareGdGiaoDichDiemListModel(GdGiaoDichDiemSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchGdGiaoDichDiems(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new GdGiaoDichDiemListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<GdGiaoDichDiemModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<GdGiaoDichDiemModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public GdGiaoDichDiemModel PrepareGdGiaoDichDiemModel(GdGiaoDichDiemModel model, GdGiaoDichDiem item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<GdGiaoDichDiemModel>();
            }
            //more
           
            return model;
        }
       public void PrepareGdGiaoDichDiem(GdGiaoDichDiemModel model, GdGiaoDichDiem item)
        {
		item.GIAO_DICH_ID = model.GIAO_DICH_ID;
		item.DIEM_DICH_VU = model.DIEM_DICH_VU;
		item.DIEM_TICH_LUY = model.DIEM_TICH_LUY;
		item.KHACH_HANG_ID = model.KHACH_HANG_ID;
		item.CONG_TRU = model.CONG_TRU;
            
        }
        #endregion	
     }
}		

