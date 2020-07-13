using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.EquipmentStatus
{
    public interface IEquipmentStatusService
    {
         Task<IPagedList<Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus>> GetAllEquipmentStatus(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus>> GetAllEquipmentStatusAsList();
           Task PrepareEquipmentStatusModel(Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus model1, object p, bool v);
       
        Task InsertEquipmentStatus(Core.Domain.EquipmentStatusEntity.EquipmentStatus equipmentStatus);
        Task<Core.Domain.EquipmentStatusEntity.EquipmentStatus> GetEquipmentStatusById(string Id);
        Task UpdateEquipmentStatus(Core.Domain.EquipmentStatusEntity.EquipmentStatus equipmentStatus);
    }
}
