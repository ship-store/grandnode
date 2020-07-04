using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Priority
{
    public interface IPriorityService
    {
         Task<IPagedList<Grand.Core.Domain.PriorityEntity.Priority>> GetAllPriorities(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.PriorityEntity.Priority>> GetAllPriorityAsList();
           Task PreparePriorityModel(Grand.Core.Domain.PriorityEntity.Priority model1, object p, bool v);
       
        Task InsertPriority(Core.Domain.PriorityEntity.Priority priority);
    }
}
