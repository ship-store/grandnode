using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Cbm
{
    public interface ICbmService
    {
         Task<IPagedList<Grand.Core.Domain.CbmEntity.CBM>> GetAllCbm(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.CbmEntity.CBM>> GetAllCbmAsList();
         Task PrepareCbmModel(Grand.Core.Domain.CbmEntity.CBM model1, object p, bool v);

        Task InsertCbm(Core.Domain.CbmEntity.CBM cbm);

    }
}
