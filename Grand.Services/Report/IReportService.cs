using Grand.Core;
using Grand.Core.Domain.UnplannedJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Report
{
    public interface IReportService
    {
        //Task<IPagedList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>> GetAllUnplannedJobs(string name = "",
        //     int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        //Task<IList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>> GetAllUnplannedJobsAsList();
        //Task PrepareUnplannedJobModel(Grand.Core.Domain.UnplannedJobs.UnplannedJob model1, object p, bool v);

        Task InsertReport(Core.Domain.Report.Report report);
        //Task<Core.Domain.UnplannedJobs.UnplannedJob> GetUnplannedJobById(string Id);
        //Task UpdateUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedJob);
        //Task DeleteUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedJob);
        Task InsertDueJobReport(Core.Domain.DueJobReport.DueJobReport dueJobReport);
        Task InsertBreakdownJobReport(Core.Domain.BreakdownJobReport.BreakdownJobReport breakdownJobReport);
        Task<IPagedList<Grand.Core.Domain.Report.Report>> GetAllUnplannedJobReports(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<Grand.Core.Domain.BreakdownJobReport.BreakdownJobReport>> GetAllBreakdownJobReports(string name = "",
          int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Task<IPagedList<Grand.Core.Domain.DueJobReport.DueJobReport>> GetAllDueJobReports(string name = "",
       int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
    }
}
