//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/9/2020
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
    public partial class KhNhomHeThongService:IKhNhomHeThongService
	{				
         #region Fields
    		private readonly IEventPublisher _eventPublisher;
            private readonly ICacheKeyService _cacheKeyService;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<KhNhomHeThong> _itemRepository;
         #endregion
         
         #region Ctor

        public KhNhomHeThongService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,           
            IRepository<KhNhomHeThong> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;   
            this._itemRepository = itemRepository;
        }

        #endregion
        
        #region Methods
        public virtual IList<KhNhomHeThong> GetAllKhNhomHeThongs(int StoreId=0){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <KhNhomHeThong> SearchKhNhomHeThongs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue){
             var query = _itemRepository.Table;
             return new PagedList<KhNhomHeThong>(query, pageIndex, pageSize);;
         }    
        
        public virtual KhNhomHeThong GetKhNhomHeThongById(int Id){
              if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
         }
         
        public virtual IList<KhNhomHeThong> GetKhNhomHeThongByIds(int[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }            
        
        public virtual void InsertKhNhomHeThong(KhNhomHeThong entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateKhNhomHeThong(KhNhomHeThong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKhNhomHeThong(KhNhomHeThong entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        } 
        
        #endregion	
     }
}		

