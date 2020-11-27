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
    public class MarMarketingHeThongMapModelFactory:IMarMarketingHeThongMapModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IMarMarketingHeThongMapService _itemService;
            
         #endregion
         
         #region Ctor

        public MarMarketingHeThongMapModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IMarMarketingHeThongMapService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region MarMarketingHeThongMap
        public MarMarketingHeThongMapSearchModel PrepareMarMarketingHeThongMapSearchModel(MarMarketingHeThongMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public MarMarketingHeThongMapListModel PrepareMarMarketingHeThongMapListModel(MarMarketingHeThongMapSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarMarketingHeThongMaps(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new MarMarketingHeThongMapListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarMarketingHeThongMapModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<MarMarketingHeThongMapModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public MarMarketingHeThongMapModel PrepareMarMarketingHeThongMapModel(MarMarketingHeThongMapModel model, MarMarketingHeThongMap item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MarMarketingHeThongMapModel>();
            }
            //more
           
            return model;
        }
       public void PrepareMarMarketingHeThongMap(MarMarketingHeThongMapModel model, MarMarketingHeThongMap item)
        {
		item.MARKETING_ID = model.MARKETING_ID;
		item.MAR_HE_THONG_ID = model.MAR_HE_THONG_ID;
            
        }
        #endregion	
     }
}		

