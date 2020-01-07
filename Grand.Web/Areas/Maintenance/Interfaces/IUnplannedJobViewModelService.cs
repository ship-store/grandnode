using Grand.Core;
using Grand.Core.Domain.UnplannedJobs;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IUnplannedJobViewModelService
    {
        Task PrepareUnplannedJobModel(UnplannedJobModel model1, object p, bool v);
        Task<IPagedList<UnplannedJob>> GetAllUnplannedJobs(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<UnplannedJob>> GetAllUnplannedJobsAsList(string id);
        Task DeleteUnplannedJob(UnplannedJob unplannedjob);
    }
   
}
