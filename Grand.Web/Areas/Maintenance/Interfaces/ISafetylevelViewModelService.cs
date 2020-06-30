using Grand.Core;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.SafetyLevelEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface ISafetyLevelViewModelService
    {
        Task<IPagedList<SafetyLevel>> GetAllSafetyLevels(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<SafetyLevel>> GetAllSafetyLevelAsList(string id);
        
        Task PrepareSafetyLevelModel(SafetyLevelModel model1, object p, bool v);
    }
}
