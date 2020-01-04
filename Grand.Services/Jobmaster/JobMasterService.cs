using Grand.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grand.Services.JobMaster;
//using Grand.Core.Domain.JobMaster;

namespace Grand.Services.JobMaster
{
    public class JobMasterService:IJobMasterService
    {
        private readonly IRepository<Grand.Core.Domain.JobMaster> _jobmasterRepository;
        public JobMasterService(IRepository<Grand.Core.Domain.JobMaster> _jobmasterRepository)
        {
            this._jobmasterRepository = _jobmasterRepository;
        }
        public Task InsertJobMaster(Core.Domain.JobMaster jobmaster)
        {
            return _jobmasterRepository.InsertAsync(jobmaster);
        }
    }
}
