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
    public class DvDieuChinhGiaModelFactory:IDvDieuChinhGiaModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDvDieuChinhGiaService _itemService;
            
         #endregion
         
         #region Ctor

        public DvDieuChinhGiaModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IDvDieuChinhGiaService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region DvDieuChinhGia
        public DvDieuChinhGiaSearchModel PrepareDvDieuChinhGiaSearchModel(DvDieuChinhGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DvDieuChinhGiaListModel PrepareDvDieuChinhGiaListModel(DvDieuChinhGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDvDieuChinhGias(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DvDieuChinhGiaListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<DvDieuChinhGiaModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<DvDieuChinhGiaModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public DvDieuChinhGiaModel PrepareDvDieuChinhGiaModel(DvDieuChinhGiaModel model, DvDieuChinhGia item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DvDieuChinhGiaModel>();
            }
            //more
           
            return model;
        }
       public void PrepareDvDieuChinhGia(DvDieuChinhGiaModel model, DvDieuChinhGia item)
        {
		item.NGAY_DIEU_CHINH = model.NGAY_DIEU_CHINH;
		item.DICH_VU_ID = model.DICH_VU_ID;
		item.GIA_BAN_BUON = model.GIA_BAN_BUON;
		item.GIA_BAN_LE = model.GIA_BAN_LE;
		item.GIA_1 = model.GIA_1;
		item.GIA_2 = model.GIA_2;
		item.GIA_3 = model.GIA_3;
		item.NGAY_TAO = model.NGAY_TAO;
		item.NGUOI_TAO = model.NGUOI_TAO;
            
        }
        #endregion	
     }
}		

