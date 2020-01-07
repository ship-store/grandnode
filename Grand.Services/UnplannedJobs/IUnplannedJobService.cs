using Grand.Core;
using Grand.Core.Domain.UnplannedJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.UnplannedJobs
{
    public interface IUnplannedJobService
    {
        Task<IPagedList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>> GetAllUnplannedJobs(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Task<IList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>> GetAllUnplannedJobsAsList();
        Task PrepareUnplannedJobModel(Grand.Core.Domain.UnplannedJobs.UnplannedJob model1, object p, bool v);

        Task InsertUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob vessel);
        Task<Core.Domain.UnplannedJobs.UnplannedJob> GetUnplannedJobById(string Id);
        Task UpdateUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedJob);
        Task DeleteUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedJob);
    }
}
