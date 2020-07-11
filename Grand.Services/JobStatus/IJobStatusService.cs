using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.JobStatus
{
    public interface IJobStatusService
    {
         Task<IPagedList<Grand.Core.Domain.JobStatusEntity.JobStatus>> GetAllJobStatus(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.JobStatusEntity.JobStatus>> GetAllJobStatusAsList();
           Task PrepareJobStatusModel(Grand.Core.Domain.JobStatusEntity.JobStatus model1, object p, bool v);
       
        Task InsertJobStatus(Core.Domain.JobStatusEntity.JobStatus jobStatus);
        Task<Core.Domain.JobStatusEntity.JobStatus> GetJobStatusById(string Id);
        Task UpdateJobStatus(Core.Domain.JobStatusEntity.JobStatus jobStatus);
    }
}
