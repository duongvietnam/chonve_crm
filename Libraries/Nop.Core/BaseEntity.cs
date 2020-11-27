using System;
using Nop.Core.Caching;

namespace Nop.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get key for caching the entity
        /// </summary>
        public string EntityCacheKey => GetEntityCacheKey(GetType(), Id);

        /// <summary>
        /// Get key for caching the entity
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="id">Entity id</param>
        /// <returns>Key for caching the entity</returns>
        public static string GetEntityCacheKey(Type entityType, object id)
        {
            return string.Format(NopCachingDefaults.NopEntityCacheKey, entityType.Name.ToLower(), id);
        }
    }
    public abstract partial class ApiBaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }

    }
    /// <summary>
    /// Các class Entity truy vấn dữ liệu từ store lên thừa kế lớp này
    /// </summary>
    public partial class ViewEntity 
    {

    }
}
