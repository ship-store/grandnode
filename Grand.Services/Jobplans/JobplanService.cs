using Grand.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Jobplan
{
    public class JobplanService : IJobplanService
    {
        private readonly IRepository<Grand.Core.Domain.Jobplan.Jobplan> _jobplanRepository;
        public JobplanService(IRepository<Grand.Core.Domain.Jobplan.Jobplan> _jobplanRepository)
        {
            this._jobplanRepository = _jobplanRepository;
        }

        public virtual async Task InsertJobplan(Grand.Core.Domain.Jobplan.Jobplan jobplan)
        {
            await _jobplanRepository.InsertAsync(jobplan);
        }
    }
}
