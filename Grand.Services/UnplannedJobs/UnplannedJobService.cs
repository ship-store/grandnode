using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.Vessel;
using Grand.Services.UnplannedJobs;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.UnplannedJobs
{
    public class UnplannedJobService : IUnplannedJobService
    {
        private readonly IRepository<Grand.Core.Domain.UnplannedJobs.UnplannedJob> _unplannedJobRepository;

        public UnplannedJobService(IRepository<Grand.Core.Domain.UnplannedJobs.UnplannedJob> _unplannedJobRepository)
        {
            this._unplannedJobRepository = _unplannedJobRepository;
        }

        async Task<IPagedList<Core.Domain.UnplannedJobs.UnplannedJob>> IUnplannedJobService.GetAllUnplannedJobs(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _unplannedJobRepository.Table;

            return await PagedList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>.Create(query, pageIndex, pageSize);
        }

        //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.UnplannedJobs.UnplannedJob>> IUnplannedJobService.GetAllUnplannedJobsAsList()
        {
            var query = _unplannedJobRepository.Table;




            return await PagedList<Grand.Core.Domain.UnplannedJobs.UnplannedJob>.Create(query, 0, 15);
        }

        Task IUnplannedJobService.PrepareUnplannedJobModel(Core.Domain.UnplannedJobs.UnplannedJob model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob vessel)
        {


            await _unplannedJobRepository.InsertAsync(vessel);


        }
        public virtual async Task UpdateUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedJob)
        {
            await _unplannedJobRepository.UpdateAsync(unplannedJob);
        }
        public virtual Task<Core.Domain.UnplannedJobs.UnplannedJob> GetUnplannedJobById(string unplannedjobId)
        {
            return _unplannedJobRepository.GetByIdAsync(unplannedjobId);
        }
        public virtual async Task DeleteUnplannedJob(Core.Domain.UnplannedJobs.UnplannedJob unplannedjob)
        {
            if (unplannedjob == null)
                throw new ArgumentNullException("Unplannedjob");


            //deleted product
            await _unplannedJobRepository.DeleteAsync(unplannedjob);



        }
    }
}
