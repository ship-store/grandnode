using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.CbmMapping
{
    public interface ICbmMappingService
    {
         Task<IPagedList<Grand.Core.Domain.CbmEntity.CBMMapping>> GetAllCbmMapping(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Task<IList<Grand.Core.Domain.CbmEntity.CBMMapping>> GetAllCbmMappingAsList();
        Task PrepareCbmMappingModel(Grand.Core.Domain.CbmEntity.CBMMapping model2, object p, bool v);

        Task InsertCbmMapping(Core.Domain.CbmEntity.CBMMapping cbmMapping);
    }
}
