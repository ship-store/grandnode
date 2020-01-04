using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.BreakdownJob;
using Grand.Services.BreakdownJob;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.BreakdownJob
{
    public class BreakdownJobService : IBreakdownJobService
    {
        private readonly IRepository<Grand.Core.Domain.BreakdownJob.BreakdownJob> _breakdownJobRepository;
        
        public BreakdownJobService(IRepository<Grand.Core.Domain.BreakdownJob.BreakdownJob> _breakdownJobRepository)
        {
            this._breakdownJobRepository = _breakdownJobRepository;
        }
       
        async Task<IPagedList<Core.Domain.BreakdownJob.BreakdownJob>> IBreakdownJobService.GetAllBreakdownJobs(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _breakdownJobRepository.Table;
         
            return await PagedList< Grand.Core.Domain.BreakdownJob.BreakdownJob>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.BreakdownJob.BreakdownJob>> IBreakdownJobService.GetAllBreakdownJobsAsList()
        {
            var query = _breakdownJobRepository.Table;


           

            return await PagedList<Grand.Core.Domain.BreakdownJob.BreakdownJob>.Create(query ,0,15);
        }

        Task IBreakdownJobService.PrepareBreakdownJobModel(Core.Domain.BreakdownJob.BreakdownJob model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertBreakdownJob(Core.Domain.BreakdownJob.BreakdownJob vessel)
        {


            await _breakdownJobRepository.InsertAsync(vessel);


        }

        public virtual Task<Core.Domain.BreakdownJob.BreakdownJob> GetBreakdownJobById(string breakdownjobId)
        {
            return _breakdownJobRepository.GetByIdAsync(breakdownjobId);
        }
        public virtual async Task UpdateBreakdownJob(Core.Domain.BreakdownJob.BreakdownJob breakdownjob)
        {
            await _breakdownJobRepository.UpdateAsync(breakdownjob);
        }
    }
}
