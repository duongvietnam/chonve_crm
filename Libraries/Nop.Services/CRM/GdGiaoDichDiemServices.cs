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
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Caching;
using Nop.Services.Caching.Extensions;
using Nop.Core.Domain.CRM;

namespace Nop.Services.CRM
{
    public partial class GdGiaoDichDiemService:IGdGiaoDichDiemService
	{				
         #region Fields
    		private readonly IEventPublisher _eventPublisher;
            private readonly ICacheKeyService _cacheKeyService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<GdGiaoDichDiem> _itemRepository;
         #endregion
         
         #region Ctor

        public GdGiaoDichDiemService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,           
            IRepository<GdGiaoDichDiem> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;   
            this._itemRepository = itemRepository;
        }

        #endregion
        
        #region Methods
        public virtual IList<GdGiaoDichDiem> GetAllGdGiaoDichDiems(int StoreId=0){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <GdGiaoDichDiem> SearchGdGiaoDichDiems(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue){
             var query = _itemRepository.Table;
             return new PagedList<GdGiaoDichDiem>(query, pageIndex, pageSize);;
         }    
        
        public virtual GdGiaoDichDiem GetGdGiaoDichDiemById(int Id){
              if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
         }
         
        public virtual IList<GdGiaoDichDiem> GetGdGiaoDichDiemByIds(int[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }            
        
        public virtual void InsertGdGiaoDichDiem(GdGiaoDichDiem entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateGdGiaoDichDiem(GdGiaoDichDiem entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteGdGiaoDichDiem(GdGiaoDichDiem entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

