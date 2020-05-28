using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.JobType
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IRepository<Grand.Core.Domain.JobTypeEntity.JobType> _jobTypeRepository;
        
        public JobTypeService(IRepository<Grand.Core.Domain.JobTypeEntity.JobType> _jobTypeRepository)
        {
            this._jobTypeRepository = _jobTypeRepository;
        }
       
        async Task<IPagedList<Core.Domain.JobTypeEntity.JobType>> IJobTypeService.GetAllJobTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _jobTypeRepository.Table;
         
            return await PagedList< Grand.Core.Domain.JobTypeEntity.JobType>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.JobTypeEntity.JobType>> IJobTypeService.GetAllJobTypeAsList()
        {
            var query = _jobTypeRepository.Table;


           

            return await PagedList<Grand.Core.Domain.JobTypeEntity.JobType>.Create(query ,0,15);
        }

        Task IJobTypeService.PrepareJobTypeModel(Core.Domain.JobTypeEntity.JobType model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertJobType(Core.Domain.JobTypeEntity.JobType jobType)
        {


            await _jobTypeRepository.InsertAsync(jobType);


        }

    }
}
