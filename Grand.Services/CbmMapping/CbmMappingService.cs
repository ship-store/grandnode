using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.CbmMapping
{
    public class CbmMappingService : ICbmMappingService
    {

        private readonly IRepository<Grand.Core.Domain.CbmEntity.CBMMapping> _CbmMappingRepository;

    
        public CbmMappingService(IRepository<Grand.Core.Domain.CbmEntity.CBMMapping> _CBMMappingRepository)
        {
            this._CbmMappingRepository = _CBMMappingRepository;
        }

        async Task<IPagedList<Core.Domain.CbmEntity.CBMMapping>> ICbmMappingService.GetAllCbmMapping(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _CbmMappingRepository.Table;
            return await PagedList< Grand.Core.Domain.CbmEntity.CBMMapping>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.CbmEntity.CBMMapping>> ICbmMappingService.GetAllCbmMappingAsList()
        {
            var query = _CbmMappingRepository.Table; 
            return await PagedList<Grand.Core.Domain.CbmEntity.CBMMapping>.Create(query ,0,15);
        }

        Task ICbmMappingService.PrepareCbmMappingModel(Core.Domain.CbmEntity.CBMMapping model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertCbmMapping(Core.Domain.CbmEntity.CBMMapping cbmMapping)
        {
            await _CbmMappingRepository.InsertAsync(cbmMapping);
        }

    }
}
