using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.JobType
{
    public interface IJobTypeService
    {
         Task<IPagedList<Grand.Core.Domain.JobType.JobType>> GetAllJobTypes(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.JobType.JobType>> GetAllJobTypeAsList();
           Task PrepareJobTypeModel(Grand.Core.Domain.JobType.JobType model1, object p, bool v);
       
        Task InsertJobType(Core.Domain.JobType.JobType jobType);
        Task<Core.Domain.JobType.JobType> GetJobTypeById(string Id);
        Task UpdateJobType(Core.Domain.JobType.JobType jobType);
    }
}
