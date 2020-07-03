using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Frequency
{
    public class FrequencyService : IFrequencyService
    {
        private readonly IRepository<Grand.Core.Domain.FrequencyEntity.Frequency> _frequencyRepository;
        
        public FrequencyService(IRepository<Grand.Core.Domain.FrequencyEntity.Frequency> _frequencyRepository)
        {
            this._frequencyRepository = _frequencyRepository;
        }
       
        async Task<IPagedList<Core.Domain.FrequencyEntity.Frequency>> IFrequencyService.GetAllFrequencies(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _frequencyRepository.Table;
         
            return await PagedList< Grand.Core.Domain.FrequencyEntity.Frequency>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.FrequencyEntity.Frequency>> IFrequencyService.GetAllFrequencyAsList()
        {
            var query = _frequencyRepository.Table;


           

            return await PagedList<Grand.Core.Domain.FrequencyEntity.Frequency>.Create(query ,0,15);
        }

        Task IFrequencyService.PrepareFrequencyModel(Core.Domain.FrequencyEntity.Frequency model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertFrequency(Core.Domain.FrequencyEntity.Frequency frequency)
        {


            await _frequencyRepository.InsertAsync(frequency);


        }

    }
}
