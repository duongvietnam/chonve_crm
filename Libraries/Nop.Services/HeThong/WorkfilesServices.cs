//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Services.Events;
using Nop.Core.Domain.DanhMuc;
using Nop.Services.Caching;
using Nop.Services.Caching.Extensions;
using Nop.Core.Domain.HeThong;
using Nop.Core.Infrastructure;
using Nop.Core.Configuration;

namespace Nop.Services.HeThong
{
    public partial class WorkfilesService:IWorkfilesService
	{

        #region Field
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<WorkFiles> _itemRepository;
        private readonly INopFileProvider _fileProvider;
        private readonly NopConfig _nopConfig;
        #endregion

        #region Ctor

        public WorkfilesService(
            NopConfig nopConfig,
            INopFileProvider fileProvider,
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<WorkFiles> itemRepository
            )
        {
            this._nopConfig = nopConfig;
            this._fileProvider = fileProvider;
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion
        #region Methods
        public virtual IList<WorkFiles> GetAllWorkfiless(){
            var query = _itemRepository.Table;
             return query.ToList();
         }
        
        public virtual IPagedList <WorkFiles> SearchWorkfiless(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<WorkFiles>(query, pageIndex, pageSize);;
         }    
        
        public virtual WorkFiles GetWorkfilesById(int Id)
        {
              if (Id == 0)
                return null;
           
            return _itemRepository.ToCachedGetById(Id);
         }
        public virtual WorkFiles GetFileByGuid(Guid guid)
        {
            var key= _cacheKeyService.PrepareKeyForDefaultCache(new CacheKey("Nop.workfiles.guid-{0}", "Nop.workfiles.guid"), guid.ToString());
            return _staticCacheManager.Get(key, () =>
            {
                return _itemRepository.Table.FirstOrDefault(c => c.Guid == guid);
            });            
        }
        public virtual IList<WorkFiles> GetWorkfilesByIds(string ids)
        {
            var itemIds = ids.Split(',').Select(c => Convert.ToInt32(c)).ToArray();
            return GetWorkfilesByIds(itemIds);
        }

        public virtual IList<WorkFiles> GetWorkfilesByIds(int[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }            
        
        public virtual void InsertWorkfiles(WorkFiles entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateWorkfiles(WorkFiles entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteWorkfiles(WorkFiles entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
            //xoa file vat ly
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(_nopConfig.FolderWorkFiles), entity.NgayTao.ToPathFolderStore(), entity.Guid.ToString() + entity.DuoiFile);
            _fileProvider.DeleteFile(_fileStore, true, _nopConfig.FolderTrashFiles);
        }
        public void DeleteWorkfiles(int? Id)
        {
            if(Id.HasValue)
            {
                var item = _itemRepository.Table.FirstOrDefault(c=>c.Id==Id);
                if(item!=null)
                    DeleteWorkfiles(item);
            }    
            
        }    
        #endregion
    }
}		

