using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.MaintenanceType
{
    public interface IMaintenanceTypeService
    {
         Task<IPagedList<Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType>> GetAllMaintenanceTypes(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType>> GetAllMaintenanceTypeAsList();
           Task PrepareMaintenanceTypeModel(Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType model1, object p, bool v);
       
        Task InsertMaintenanceType(Core.Domain.MaintenanceTypeEntity.MaintenanceType maintenanceType);
        Task<Core.Domain.MaintenanceTypeEntity.MaintenanceType> GetMaintenanceTypeById(string Id);
        Task UpdateMaintenanceType(Core.Domain.MaintenanceTypeEntity.MaintenanceType maintenanceType);
    }
}
