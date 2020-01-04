using Grand.Core;
using Grand.Core.Domain;
using Grand.Services.JobMaster;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class JobMasterViewModelService : IJobMasterViewModelService
    {
        private readonly Grand.Services.JobMaster.IJobMasterService _jobmasterService;


        public JobMasterViewModelService(Grand.Services.JobMaster.IJobMasterService _jobmasterService)
        {
            this._jobmasterService = _jobmasterService;

        }
        public virtual async Task<JobMaster>InsertJobMasterModel(JobMasterModel newJobMaster)
        {
            JobMaster addNewJobMaster = new JobMaster();

            addNewJobMaster.JobCode = newJobMaster.JobCode;
            addNewJobMaster.JobType = newJobMaster.JobType;
            addNewJobMaster.JobTitle = newJobMaster.JobTitle;
            addNewJobMaster.JobDescription= newJobMaster.JobDescription;
        
                await _jobmasterService.InsertJobMaster(addNewJobMaster);
            return addNewJobMaster;
             
            }
           internal static Task GetAllJobMaster(object searchName,int v1,int pageSize,bool v2)
        {
            throw new NotImplementedException();
        }
    }
}
    

   