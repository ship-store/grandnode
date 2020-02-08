using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.Vessel;
using Grand.Services.UnplannedJobs;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Grand.Core.Domain.Report.Report> _reportRepository;
        private readonly IRepository<Grand.Core.Domain.DueJobReport.DueJobReport> _duejobreportRepository;
        private readonly IRepository<Grand.Core.Domain.BreakdownJobReport.BreakdownJobReport> _breakdownjobreportRepository;

        public ReportService(IRepository<Grand.Core.Domain.Report.Report> _reportRepository, IRepository<Grand.Core.Domain.DueJobReport.DueJobReport> _duejobreportRepository, IRepository<Grand.Core.Domain.BreakdownJobReport.BreakdownJobReport> _breakdownjobreportRepository)
        {
            this._reportRepository = _reportRepository;
            this._duejobreportRepository = _duejobreportRepository;
            this._breakdownjobreportRepository=_breakdownjobreportRepository;
        }

        //async Task<IPagedList<Core.Domain.UnplannedJobs.UnplannedJob>> IUnplannedJobService.GetAllUnplannedJobs(string name, int pageIndex, int pageSize, bool showHidden)
        //{
        //    var query = _unplannedJobRepository.Table;

        //    return await PagedList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>.Create(query, pageIndex, pageSize);
        //}

        ////TODO
        //// page size paramater need tobe setted
        //async Task<IList<Core.Domain.UnplannedJobs.UnplannedJob>> IUnplannedJobService.GetAllUnplannedJobsAsList()
        //{
        //    var query = _unplannedJobRepository.Table;




        //    return await PagedList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>.Create(query, 0, 15);
        //}

        //Task IUnplannedJobService.PrepareUnplannedJobModel(Core.Domain.UnplannedJobs.UnplannedJob model1, object p, bool v)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual async Task InsertReport(Core.Domain.Report.Report report)
        {


            await _reportRepository.InsertAsync(report);


        }
        public virtual async Task InsertDueJobReport(Core.Domain.DueJobReport.DueJobReport dueJobReport)
        {


            await _duejobreportRepository.InsertAsync(dueJobReport);


        }

        public virtual async Task InsertBreakdownJobReport(Core.Domain.BreakdownJobReport.BreakdownJobReport breakdownJobReport)
        {


            await _breakdownjobreportRepository.InsertAsync(breakdownJobReport);


        }
        async Task<IPagedList<Core.Domain.Report.Report>> IReportService.GetAllUnplannedJobReports(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _reportRepository.Table;

            return await PagedList<Grand.Core.Domain.Report.Report>.Create(query, pageIndex, pageSize);
        }

        async Task<IPagedList<Core.Domain.BreakdownJobReport.BreakdownJobReport>> IReportService.GetAllBreakdownJobReports(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _breakdownjobreportRepository.Table;

            return await PagedList<Grand.Core.Domain.BreakdownJobReport.BreakdownJobReport>.Create(query, pageIndex, pageSize);
        }


        async Task<IPagedList<Core.Domain.DueJobReport.DueJobReport>> IReportService.GetAllDueJobReports(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _duejobreportRepository.Table;

            return await PagedList<Grand.Core.Domain.DueJobReport.DueJobReport>.Create(query, pageIndex, pageSize);
        }
        //public virtual async Task UpdateUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedJob)
        //{
        //    await _unplannedJobRepository.UpdateAsync(unplannedJob);
        //}
        //public virtual Task<Core.Domain.UnplannedJobs.UnplannedJob> GetUnplannedJobById(string unplannedjobId)
        //{
        //    return _unplannedJobRepository.GetByIdAsync(unplannedjobId);
        //}
        //public virtual async Task DeleteUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedjob)
        //{
        //    if (unplannedjob == null)
        //        throw new ArgumentNullException("Unplannedjob");


        //    //deleted product
        //    await _unplannedJobRepository.DeleteAsync(unplannedjob);



        //}

    }
}
