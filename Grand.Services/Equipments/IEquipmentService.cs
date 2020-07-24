
using Grand.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Equipments
{
    public interface IEquipmentService
    {
        Task<IPagedList<Grand.Core.Domain.Equipment.Equipment>> GetAllEquipment(string name = "",
          int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task InsertEquipment(Grand.Core.Domain.Equipment.Equipment equipment);
        Task<Grand.Core.Domain.Equipment.Equipment> GetEquipmentById(string Id);
        Task UpdateEquipment(Grand.Core.Domain.Equipment.Equipment equipment);

        Task RemoveEquipment(Grand.Core.Domain.Equipment.Equipment equipment);
    }
}
