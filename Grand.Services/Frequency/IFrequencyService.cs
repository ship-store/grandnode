using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Frequency
{
    public interface IFrequencyService
    {
         Task<IPagedList<Grand.Core.Domain.FrequencyEntity.Frequency>> GetAllFrequencies(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.FrequencyEntity.Frequency>> GetAllFrequencyAsList();
           Task PrepareFrequencyModel(Grand.Core.Domain.FrequencyEntity.Frequency model1, object p, bool v);
       
        Task InsertFrequency(Core.Domain.FrequencyEntity.Frequency frequency);
    }
}
