using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobStatusEntity;
using Grand.Core.Domain.JobTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.EquipmentType;
using Grand.Services.JobStatus;
using Grand.Services.JobType;
using Grand.Services.Maker;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class JobStatusViewModelService : IJobStatusViewModelService
    {
        private readonly IJobStatusService _jobStatusService;
        public JobStatusViewModelService(IJobStatusService _jobStatusService)
        {
            this._jobStatusService = _jobStatusService;
            
        }
        Task<IPagedList<JobStatus>> IJobStatusViewModelService.GetAllJobStatus(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<JobStatus>> IJobStatusViewModelService.GetAllJobStatusAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IJobStatusViewModelService.PrepareJobStatusModel(JobStatusModel addNewJobStatus, object p, bool v)
        {
            try
            {

                var jobStatus = new JobStatus();

                jobStatus.Status = addNewJobStatus.Status;
               
                await  _jobStatusService.InsertJobStatus(jobStatus);
            }
            catch (Exception ex)
            {
                var jobStatus = new JobStatus();

                jobStatus.Status = addNewJobStatus.Status;

                await _jobStatusService.InsertJobStatus(jobStatus);

            }
        }
    }
}
