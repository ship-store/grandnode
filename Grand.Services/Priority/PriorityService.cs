using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Priority
{
    public class PriorityService : IPriorityService
    {
        private readonly IRepository<Grand.Core.Domain.PriorityEntity.Priority> _priorityRepository;
        
        public PriorityService(IRepository<Grand.Core.Domain.PriorityEntity.Priority> _priorityRepository)
        {
            this._priorityRepository = _priorityRepository;
        }
       
        async Task<IPagedList<Core.Domain.PriorityEntity.Priority>> IPriorityService.GetAllPriorities(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _priorityRepository.Table;
         
            return await PagedList< Grand.Core.Domain.PriorityEntity.Priority>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.PriorityEntity.Priority>> IPriorityService.GetAllPriorityAsList()
        {
            var query = _priorityRepository.Table;


           

            return await PagedList<Grand.Core.Domain.PriorityEntity.Priority>.Create(query ,0,15);
        }

        Task IPriorityService.PreparePriorityModel(Core.Domain.PriorityEntity.Priority model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertPriority(Core.Domain.PriorityEntity.Priority priority)
        {


            await _priorityRepository.InsertAsync(priority);


        }
        public virtual async Task UpdatePriority(Core.Domain.PriorityEntity.Priority priority)
        {
            await _priorityRepository.UpdateAsync(priority);
        }
        public virtual Task<Core.Domain.PriorityEntity.Priority> GetPriorityById(string priorityId)
        {
            return _priorityRepository.GetByIdAsync(priorityId);
        }


    }
}
