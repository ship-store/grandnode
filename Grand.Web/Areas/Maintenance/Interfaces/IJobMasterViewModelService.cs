using Grand.Core.Domain;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
   public interface IJobMasterViewModelService
    {
        Task<JobMaster> InsertJobMasterModel(JobMasterModel model);
    }
}
