//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.HeThong;


namespace Nop.Services.HeThong
{
    public partial interface IWorkfilesService 
    {    
    #region Workfiles
        IList<WorkFiles> GetAllWorkfiless();
        IPagedList <WorkFiles> SearchWorkfiless(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        WorkFiles GetWorkfilesById(int Id);
        WorkFiles GetFileByGuid(Guid guid);
        IList<WorkFiles> GetWorkfilesByIds(string ids);
        IList<WorkFiles> GetWorkfilesByIds(int[] newsIds);
        void InsertWorkfiles(WorkFiles entity);
        void UpdateWorkfiles(WorkFiles entity);
        void DeleteWorkfiles(WorkFiles entity);
        void DeleteWorkfiles(int? Id);
        #endregion
    }
}
