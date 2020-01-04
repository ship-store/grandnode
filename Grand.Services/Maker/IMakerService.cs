using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Maker
{
    public interface IMakerService
    {
         Task<IPagedList<Grand.Core.Domain.MakerEntity.Maker>> GetAllMakers(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.MakerEntity.Maker>> GetAllMakerAsList();
           Task PrepareMakerModel(Grand.Core.Domain.MakerEntity.Maker model1, object p, bool v);
       
        Task InsertMaker(Core.Domain.MakerEntity.Maker maker);
    }
}
