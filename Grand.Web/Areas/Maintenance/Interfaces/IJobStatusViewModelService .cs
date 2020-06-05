using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobStatusEntity;
using Grand.Core.Domain.JobType;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IJobStatusViewModelService
    {
        Task<IPagedList<JobStatus>> GetAllJobStatus(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<JobStatus>> GetAllJobStatusAsList(string id);
        Task PrepareJobStatusModel(JobStatusModel model1, object p, bool v);
    }
}
