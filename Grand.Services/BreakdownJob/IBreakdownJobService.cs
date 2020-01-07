using Grand.Core;
using Grand.Core.Domain.BreakdownJob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.BreakdownJob
{
    public interface IBreakdownJobService
    {
         Task<IPagedList<Grand.Core.Domain.BreakdownJob.BreakdownJob>> GetAllBreakdownJobs(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.BreakdownJob.BreakdownJob>> GetAllBreakdownJobsAsList();
         Task PrepareBreakdownJobModel(Grand.Core.Domain.BreakdownJob.BreakdownJob model1, object p, bool v);
       
        Task InsertBreakdownJob(Core.Domain.BreakdownJob.BreakdownJob vessel);
        Task<Core.Domain.BreakdownJob.BreakdownJob> GetBreakdownJobById(string Id);
        Task UpdateBreakdownJob(Core.Domain.BreakdownJob.BreakdownJob breakdownjob);
        Task DeleteBreakdownJob(Core.Domain.BreakdownJob.BreakdownJob breakdownjob);
    }
}
