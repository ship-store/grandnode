using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.EquipmentType
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly IRepository<Grand.Core.Domain.EquipmentTypeEntity.EquipmentType> _equipmentTypeRepository;
        
        public EquipmentTypeService(IRepository<Grand.Core.Domain.EquipmentTypeEntity.EquipmentType> _equipmentTypeRepository)
        {
            this._equipmentTypeRepository = _equipmentTypeRepository;
        }
       
        async Task<IPagedList<Core.Domain.EquipmentTypeEntity.EquipmentType>> IEquipmentTypeService.GetAllEquipmentTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _equipmentTypeRepository.Table;
         
            return await PagedList< Grand.Core.Domain.EquipmentTypeEntity.EquipmentType>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.EquipmentTypeEntity.EquipmentType>> IEquipmentTypeService.GetAllEquipmentTypeAsList()
        {
            var query = _equipmentTypeRepository.Table;


           

            return await PagedList<Grand.Core.Domain.EquipmentTypeEntity.EquipmentType>.Create(query ,0,15);
        }

        Task IEquipmentTypeService.PrepareEquipmentTypeModel(Core.Domain.EquipmentTypeEntity.EquipmentType model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertEquipmentType(Core.Domain.EquipmentTypeEntity.EquipmentType equipmentType)
        {


            await _equipmentTypeRepository.InsertAsync(equipmentType);


        }
        public virtual Task<Core.Domain.EquipmentTypeEntity.EquipmentType> GetEquipmentTypeById(string equipmentTypeId)
        {
            return _equipmentTypeRepository.GetByIdAsync(equipmentTypeId);
        }
        public virtual async Task UpdateEquipmentType(Core.Domain.EquipmentTypeEntity.EquipmentType equipmentType)
        {
            await _equipmentTypeRepository.UpdateAsync(equipmentType);
        }


    }
}
