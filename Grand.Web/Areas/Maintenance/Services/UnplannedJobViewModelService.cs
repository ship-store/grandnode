using Grand.Core;
using Grand.Core.Domain.UnplannedJobs;
using Grand.Services.UnplannedJobs;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class UnplannedJobViewModelService : IUnplannedJobViewModelService
    {
        private readonly IUnplannedJobService _unplannedJobService;
        public UnplannedJobViewModelService(IUnplannedJobService _unplannedJobService)
        {
            this._unplannedJobService = _unplannedJobService;
        }

        Task<IPagedList<UnplannedJob>> IUnplannedJobViewModelService.GetAllUnplannedJobs(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }
        Task<IPagedList<UnplannedJob>> IUnplannedJobViewModelService.GetAllUnplannedJobsAsList(string id)
        {
            throw new NotImplementedException();
        }
        public virtual async Task PrepareUnplannedJobModel(UnplannedJobModel model1, object p, bool v)
        {
            try
            {

                var unplannedJob = new UnplannedJob();

                unplannedJob.EquipmentName = model1.EquipmentName;
                unplannedJob.JobOrder = model1.JobOrder;
                unplannedJob.Title = model1.Title;
                unplannedJob.JobReportedDate = model1.JobReportedDate;
                unplannedJob.ReportedBy = model1.ReportedBy;
                unplannedJob.Status = model1.Status;
                unplannedJob.Vessel = model1.Vessel;
                unplannedJob.DeleteStatus = null;

                await _unplannedJobService.InsertUnplannedJob(unplannedJob);
               
            }
            catch (Exception)
            {
                var unplannedJob = new UnplannedJob();
                unplannedJob.EquipmentName = model1.EquipmentName;
                unplannedJob.JobOrder = model1.JobOrder;
                unplannedJob.Title = model1.Title;
                unplannedJob.JobReportedDate = model1.JobReportedDate;
                unplannedJob.ReportedBy = model1.ReportedBy;
                unplannedJob.Status = model1.Status;
                unplannedJob.Vessel = model1.Vessel;
                unplannedJob.DeleteStatus = null;

                await _unplannedJobService.InsertUnplannedJob(unplannedJob);

            }
        }
        public async Task DeleteUnplannedJob(UnplannedJob unplannedjob)
        {
            await _unplannedJobService.DeleteUnplannedJob(unplannedjob);
        }
    }
}


