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
    public class DvNhatKyGiaModelFactory:IDvNhatKyGiaModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDvNhatKyGiaService _itemService;
            
         #endregion
         
         #region Ctor

        public DvNhatKyGiaModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IDvNhatKyGiaService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region DvNhatKyGia
        public DvNhatKyGiaSearchModel PrepareDvNhatKyGiaSearchModel(DvNhatKyGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DvNhatKyGiaListModel PrepareDvNhatKyGiaListModel(DvNhatKyGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDvNhatKyGias(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DvNhatKyGiaListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<DvNhatKyGiaModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<DvNhatKyGiaModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public DvNhatKyGiaModel PrepareDvNhatKyGiaModel(DvNhatKyGiaModel model, DvNhatKyGia item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DvNhatKyGiaModel>();
            }
            //more
           
            return model;
        }
       public void PrepareDvNhatKyGia(DvNhatKyGiaModel model, DvNhatKyGia item)
        {
		item.DICH_VU_ID = model.DICH_VU_ID;
		item.NGAY_SUA = model.NGAY_SUA;
		item.GIA_BAN_BUON = model.GIA_BAN_BUON;
		item.GIA_BAN_LE = model.GIA_BAN_LE;
		item.GIA_1 = model.GIA_1;
		item.GIA_2 = model.GIA_2;
		item.GIA_3 = model.GIA_3;
            
        }
        #endregion	
     }
}		

