using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.EquipmentType
{
    public interface IEquipmentTypeService
    {
         Task<IPagedList<Grand.Core.Domain.EquipmentTypeEntity.EquipmentType>> GetAllEquipmentTypes(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.EquipmentTypeEntity.EquipmentType>> GetAllEquipmentTypeAsList();
           Task PrepareEquipmentTypeModel(Grand.Core.Domain.EquipmentTypeEntity.EquipmentType model1, object p, bool v);
       
        Task InsertEquipmentType(Core.Domain.EquipmentTypeEntity.EquipmentType equipmentType);
        Task<Core.Domain.EquipmentTypeEntity.EquipmentType> GetEquipmentTypeById(string Id);
        Task UpdateEquipmentType(Core.Domain.EquipmentTypeEntity.EquipmentType equipmentType);
    }
}
