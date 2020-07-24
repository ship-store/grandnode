using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Critical
{
    public interface ICriticalService
    {
         Task<IPagedList<Grand.Core.Domain.CriticalEntity.Critical>> GetAllCriticals(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.CriticalEntity.Critical>> GetAllCriticalAsList();
           Task PrepareCriticalModel(Grand.Core.Domain.CriticalEntity.Critical model1, object p, bool v);
       
        Task InsertCritical(Core.Domain.CriticalEntity.Critical critical);
        Task<Core.Domain.CriticalEntity.Critical> GetCriticalById(string Id);
        Task UpdateCritical(Core.Domain.CriticalEntity.Critical critical);
    }
}
