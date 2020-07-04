using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.FrequencyType
{
    public interface IFrequencyTypeService
    {
         Task<IPagedList<Grand.Core.Domain.FrequencyTypeEntity.FrequencyType>> GetAllFrequencyTypes(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.FrequencyTypeEntity.FrequencyType>> GetAllFrequencyTypeAsList();
           Task PrepareFrequencyTypeModel(Grand.Core.Domain.FrequencyTypeEntity.FrequencyType model1, object p, bool v);
       
        Task InsertFrequencyType(Core.Domain.FrequencyTypeEntity.FrequencyType frequencyType);
    }
}
