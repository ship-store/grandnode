using Grand.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Jobplan
{
    public interface IJobplanService
    {
        Task InsertJobplan(Grand.Core.Domain.Jobplan.Jobplan jobplan);
        Task<IPagedList<Grand.Core.Domain.Jobplan.Jobplan>> GetAllJobpan(string name = "",
         int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<Core.Domain.Jobplan.Jobplan>> GetAllJobplan(string name, int pageIndex, int pageSize, bool showHidden);
    }
}
