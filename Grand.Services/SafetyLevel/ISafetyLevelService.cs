using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.SafetyLevel
{
    public interface ISafetyLevelService
    {
         Task<IPagedList<Grand.Core.Domain.SafetyLevelEntity.SafetyLevel>> GetAllSafetyLevels(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.SafetyLevelEntity.SafetyLevel>> GetAllSafetyLevelAsList();
           Task PrepareSafetyLevelModel(Grand.Core.Domain.SafetyLevelEntity.SafetyLevel model1, object p, bool v);
       
        Task InsertSafetyLevel(Core.Domain.SafetyLevelEntity.SafetyLevel safetyLevel);
    }
}
