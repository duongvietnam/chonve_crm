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
    public class GdGiaoDichSubModelFactory:IGdGiaoDichSubModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IGdGiaoDichSubService _itemService;
            
         #endregion
         
         #region Ctor

        public GdGiaoDichSubModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IGdGiaoDichSubService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region GdGiaoDichSub
        public GdGiaoDichSubSearchModel PrepareGdGiaoDichSubSearchModel(GdGiaoDichSubSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public GdGiaoDichSubListModel PrepareGdGiaoDichSubListModel(GdGiaoDichSubSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchGdGiaoDichSubs(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new GdGiaoDichSubListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<GdGiaoDichSubModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<GdGiaoDichSubModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public GdGiaoDichSubModel PrepareGdGiaoDichSubModel(GdGiaoDichSubModel model, GdGiaoDichSub item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<GdGiaoDichSubModel>();
            }
            //more
           
            return model;
        }
       public void PrepareGdGiaoDichSub(GdGiaoDichSubModel model, GdGiaoDichSub item)
        {
		item.GIAO_DICH_ID = model.GIAO_DICH_ID;
		item.SO_LUONG = model.SO_LUONG;
		item.DICH_VU_ID = model.DICH_VU_ID;
		item.DON_VI_TINH = model.DON_VI_TINH;
		item.SO_TIEN = model.SO_TIEN;
		item.DIEM_DICH_VU = model.DIEM_DICH_VU;
            
        }
        #endregion	
     }
}		

