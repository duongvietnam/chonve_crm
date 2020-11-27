//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 26/11/2020
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
    public class CdChuyenDiModelFactory:ICdChuyenDiModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly ICdChuyenDiService _itemService;
            
         #endregion
         
         #region Ctor

        public CdChuyenDiModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            ICdChuyenDiService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region CdChuyenDi
        public CdChuyenDiSearchModel PrepareCdChuyenDiSearchModel(CdChuyenDiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public CdChuyenDiListModel PrepareCdChuyenDiListModel(CdChuyenDiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchCdChuyenDis(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new CdChuyenDiListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<CdChuyenDiModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<CdChuyenDiModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public CdChuyenDiModel PrepareCdChuyenDiModel(CdChuyenDiModel model, CdChuyenDi item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<CdChuyenDiModel>();
            }
            //more
           
            return model;
        }
       public void PrepareCdChuyenDi(CdChuyenDiModel model, CdChuyenDi item)
        {
		item.MA = model.MA;
		item.BIEN_SO_XE = model.BIEN_SO_XE;
		item.DICH_VU_ID = model.DICH_VU_ID;
		item.TEN_LOAI_XE = model.TEN_LOAI_XE;
		item.TEN_LAI_XE = model.TEN_LAI_XE;
		item.SO_KHACH = model.SO_KHACH;
		item.SO_GHE = model.SO_GHE;
            
        }
        #endregion	
     }
}		

