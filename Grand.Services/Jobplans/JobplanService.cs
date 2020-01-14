using Grand.Core;
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

        public Task<IPagedList<Core.Domain.Jobplan.Jobplan>> GetAllJobpan(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertJobplan(Grand.Core.Domain.Jobplan.Jobplan jobplan)
        {
            await _jobplanRepository.InsertAsync(jobplan);
        }
         async Task<IPagedList<Grand.Core.Domain.Jobplan.Jobplan>> IJobplanService.GetAllJobplan(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _jobplanRepository.Table;

            return await PagedList<Grand.Core.Domain.Jobplan.Jobplan>.Create(query, pageIndex, pageSize);
        }
    }
}
