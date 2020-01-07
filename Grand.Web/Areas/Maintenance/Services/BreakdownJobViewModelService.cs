using Grand.Core;
using Grand.Core.Domain.BreakdownJob;
using Grand.Services.BreakdownJob;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class BreakdownJobViewModelService : IBreakdownJobViewModelService
    {
        private readonly IBreakdownJobService _breakdownJobService;
        public BreakdownJobViewModelService(IBreakdownJobService _breakdownJobService)
        {
            this._breakdownJobService = _breakdownJobService;
        }
        Task<IPagedList<BreakdownJob>> IBreakdownJobViewModelService.GetAllBreakdownJobs(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<BreakdownJob>> IBreakdownJobViewModelService.GetAllBreakdownJobsAsList(string id)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteBreakdownJob(BreakdownJob breakdownjob)
        {
            await _breakdownJobService.DeleteBreakdownJob(breakdownjob);
        }
        async Task IBreakdownJobViewModelService.PrepareBreakdownJobModel(BreakdownJobModel addNewBreakdownJob, object p, bool v)
        {
            try
            {
                var breakdownJob = new BreakdownJob();

                breakdownJob.EquipmentName = addNewBreakdownJob.EquipmentName;
                breakdownJob.JobOrder = addNewBreakdownJob.JobOrder;
                breakdownJob.Title = addNewBreakdownJob.Title;
                breakdownJob.JobReportedDate = addNewBreakdownJob.JobReportedDate;
                breakdownJob.ReportedBy = addNewBreakdownJob.ReportedBy;
                breakdownJob.Status = addNewBreakdownJob.Status;

                await _breakdownJobService.InsertBreakdownJob(breakdownJob);
            }
            catch (Exception ex)
            {
                var breakdownJob = new BreakdownJob();

                breakdownJob.EquipmentName = addNewBreakdownJob.EquipmentName;
                breakdownJob.JobOrder = addNewBreakdownJob.JobOrder;
                breakdownJob.Title = addNewBreakdownJob.Title;
                breakdownJob.JobReportedDate = addNewBreakdownJob.JobReportedDate;
                breakdownJob.ReportedBy = addNewBreakdownJob.ReportedBy;
                breakdownJob.Status = addNewBreakdownJob.Status;
                
                await _breakdownJobService.InsertBreakdownJob(breakdownJob);

            }
        }
    }
}
