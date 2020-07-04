using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.FrequencyType
{
    public class FrequencyTypeService : IFrequencyTypeService
    {
        private readonly IRepository<Grand.Core.Domain.FrequencyTypeEntity.FrequencyType> _frequencyTypeRepository;
        
        public FrequencyTypeService(IRepository<Grand.Core.Domain.FrequencyTypeEntity.FrequencyType> _frequencyTypeRepository)
        {
            this._frequencyTypeRepository = _frequencyTypeRepository;
        }
       
        async Task<IPagedList<Core.Domain.FrequencyTypeEntity.FrequencyType>> IFrequencyTypeService.GetAllFrequencyTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _frequencyTypeRepository.Table;
         
            return await PagedList< Grand.Core.Domain.FrequencyTypeEntity.FrequencyType>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.FrequencyTypeEntity.FrequencyType>> IFrequencyTypeService.GetAllFrequencyTypeAsList()
        {
            var query = _frequencyTypeRepository.Table;


           

            return await PagedList<Grand.Core.Domain.FrequencyTypeEntity.FrequencyType>.Create(query ,0,15);
        }

        Task IFrequencyTypeService.PrepareFrequencyTypeModel(Core.Domain.FrequencyTypeEntity.FrequencyType model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertFrequencyType(Core.Domain.FrequencyTypeEntity.FrequencyType frequencyType)
        {


            await _frequencyTypeRepository.InsertAsync(frequencyType);


        }

    }
}
