//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
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
    public class DmSuKienModelFactory:IDmSuKienModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDmSuKienService _itemService;
            
         #endregion
         
         #region Ctor

        public DmSuKienModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IDmSuKienService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region DmSuKien
        public DmSuKienSearchModel PrepareDmSuKienSearchModel(DmSuKienSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DmSuKienListModel PrepareDmSuKienListModel(DmSuKienSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDmSuKiens(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DmSuKienListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<DmSuKienModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<DmSuKienModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public DmSuKienModel PrepareDmSuKienModel(DmSuKienModel model, DmSuKien item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DmSuKienModel>();
            }
            //more
           
            return model;
        }
       public void PrepareDmSuKien(DmSuKienModel model, DmSuKien item)
        {
		item.TEN = model.TEN;
		item.MA = model.MA;
		item.NGAY_BAT_DAU = model.NGAY_BAT_DAU;
		item.NGAY_KET_THUC = model.NGAY_KET_THUC;
		item.DOANH_NGHIEP_ID = model.DOANH_NGHIEP_ID;
            
        }
        #endregion	
     }
}		

