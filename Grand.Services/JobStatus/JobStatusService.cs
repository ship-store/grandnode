using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.JobStatus
{
    public class JobStatusService : IJobStatusService
    {
        private readonly IRepository<Grand.Core.Domain.JobStatusEntity.JobStatus> _jobStatusRepository;
        
        public JobStatusService(IRepository<Grand.Core.Domain.JobStatusEntity.JobStatus> _jobStatusRepository)
        {
            this._jobStatusRepository = _jobStatusRepository;
        }
       
        async Task<IPagedList<Core.Domain.JobStatusEntity.JobStatus>> IJobStatusService.GetAllJobStatus(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _jobStatusRepository.Table;
         
            return await PagedList< Grand.Core.Domain.JobStatusEntity.JobStatus>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.JobStatusEntity.JobStatus>> IJobStatusService.GetAllJobStatusAsList()
        {
            var query = _jobStatusRepository.Table;


           

            return await PagedList<Grand.Core.Domain.JobStatusEntity.JobStatus>.Create(query ,0,15);
        }

        Task IJobStatusService.PrepareJobStatusModel(Core.Domain.JobStatusEntity.JobStatus model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertJobStatus(Core.Domain.JobStatusEntity.JobStatus jobStatus)
        {


            await _jobStatusRepository.InsertAsync(jobStatus);


        }
        public virtual Task<Core.Domain.JobStatusEntity.JobStatus> GetJobStatusById(string jobStatusId)
        {
            return _jobStatusRepository.GetByIdAsync(jobStatusId);
        }
        public virtual async Task UpdateJobStatus(Core.Domain.JobStatusEntity.JobStatus jobStatus)
        {
            await _jobStatusRepository.UpdateAsync(jobStatus);
        }

    }
}
