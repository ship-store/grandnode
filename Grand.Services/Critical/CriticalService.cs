using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Critical
{
    public class CriticalService : ICriticalService
    {
        private readonly IRepository<Grand.Core.Domain.CriticalEntity.Critical> _criticalRepository;
        
        public CriticalService(IRepository<Grand.Core.Domain.CriticalEntity.Critical> _criticalRepository)
        {
            this._criticalRepository = _criticalRepository;
        }
       
        async Task<IPagedList<Core.Domain.CriticalEntity.Critical>> ICriticalService.GetAllCriticals(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _criticalRepository.Table;
         
            return await PagedList< Grand.Core.Domain.CriticalEntity.Critical>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.CriticalEntity.Critical>> ICriticalService.GetAllCriticalAsList()
        {
            var query = _criticalRepository.Table;


           

            return await PagedList<Grand.Core.Domain.CriticalEntity.Critical>.Create(query ,0,15);
        }

        Task ICriticalService.PrepareCriticalModel(Core.Domain.CriticalEntity.Critical model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertCritical(Core.Domain.CriticalEntity.Critical critical)
        {


            await _criticalRepository.InsertAsync(critical);


        }
        public virtual Task<Core.Domain.CriticalEntity.Critical> GetCriticalById(string criticalId)
        {
            return _criticalRepository.GetByIdAsync(criticalId);
        }
        public virtual async Task UpdateCritical(Core.Domain.CriticalEntity.Critical critical)
        {
            await _criticalRepository.UpdateAsync(critical);
        }

    }
}
