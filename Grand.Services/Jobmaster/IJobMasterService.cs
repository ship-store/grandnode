using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.JobMaster
{
    public partial interface IJobMasterService
    {
        Task InsertJobMaster(Core.Domain.JobMaster jobMaster);
    }
}
