using Grand.Core;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.FrequencyTypeEntity;
using Grand.Core.Domain.MaintenanceTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IMaintenanceTypeViewModelService
    {
        Task<IPagedList<MaintenanceType>> GetAllMaintenanceTypes(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<MaintenanceType>> GetAllMaintenanceTypeAsList(string id);
        
        Task PrepareMaintenanceTypeModel(MaintenanceTypeModel model1, object p, bool v);
    }
}
