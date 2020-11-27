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
    public class KhThongTinMoRongModelFactory:IKhThongTinMoRongModelFactory
	{				
         #region Fields    		
            private readonly IWorkContext _workContext;
            private readonly IStoreContext _storeContext;
            private readonly ILocalizationService _localizationService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IKhThongTinMoRongService _itemService;
            
         #endregion
         
         #region Ctor

        public KhThongTinMoRongModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,        
            IKhThongTinMoRongService itemService
            )
        {           
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion
        
        #region KhThongTinMoRong
        public KhThongTinMoRongSearchModel PrepareKhThongTinMoRongSearchModel(KhThongTinMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public KhThongTinMoRongListModel PrepareKhThongTinMoRongListModel(KhThongTinMoRongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhThongTinMoRongs(StoreId:_storeContext.CurrentStore.Id,Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new KhThongTinMoRongListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhThongTinMoRongModel>();
                foreach(var item in items)
                {
                    var itemModel = item.ToModel<KhThongTinMoRongModel>();
                    ls.Add(itemModel);
                }
                return ls;   
                
            });
            
            return model;
        }
        public KhThongTinMoRongModel PrepareKhThongTinMoRongModel(KhThongTinMoRongModel model, KhThongTinMoRong item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KhThongTinMoRongModel>();
            }
            //more
           
            return model;
        }
       public void PrepareKhThongTinMoRong(KhThongTinMoRongModel model, KhThongTinMoRong item)
        {
		item.KHACH_HANG_ID = model.KHACH_HANG_ID;
		item.CAU_HINH_ID = model.CAU_HINH_ID;
		item.VALUE = model.VALUE;
		item.NGAY_TAO = model.NGAY_TAO;
		item.NGUOI_TAO = model.NGUOI_TAO;
            
        }
        #endregion	
     }
}		

