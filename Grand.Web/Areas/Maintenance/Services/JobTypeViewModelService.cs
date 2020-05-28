using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.EquipmentType;
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
    public partial class JobTypeViewModelService : IJobTypeViewModelService
    {
        private readonly IJobTypeService _jobTypeService;
        public JobTypeViewModelService(IJobTypeService _jobTypeService)
        {
            this._jobTypeService = _jobTypeService;
            
        }
        Task<IPagedList<JobType>> IJobTypeViewModelService.GetAllJobTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<JobType>> IJobTypeViewModelService.GetAllJobTypeAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IJobTypeViewModelService.PrepareJobTypeModel(JobTypeModel addNewJobType, object p, bool v)
        {
            try
            {

                var jobType = new JobType();

                jobType.Job_type = addNewJobType.Job_type;
               
                await  _jobTypeService.InsertJobType(jobType);
            }
            catch (Exception ex)
            {
                var jobType = new JobType();

                jobType.Job_type = addNewJobType.Job_type;

                await _jobTypeService.InsertJobType(jobType);

            }
        }
    }
}
