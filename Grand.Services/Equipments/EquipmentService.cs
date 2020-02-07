using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.Equipment;

namespace Grand.Services.Equipments
{
    public  class EquipmentService: IEquipmentService
    {
        private readonly IRepository<Grand.Core.Domain.Equipment.Equipment> _equipmentRepository;
        public EquipmentService(IRepository<Grand.Core.Domain.Equipment.Equipment> _equipmentRepository)
        {
            this._equipmentRepository = _equipmentRepository;
        }

        public Task<Equipment> GetEquipmentById(string Id)
        {
            return _equipmentRepository.GetByIdAsync(Id);
        }

        public virtual async Task InsertEquipment(Grand.Core.Domain.Equipment.Equipment equipment)
        {
            await _equipmentRepository.InsertAsync(equipment);

        }

        async Task<IPagedList<Grand.Core.Domain.Equipment.Equipment>> IEquipmentService.GetAllEquipment(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _equipmentRepository.Table;

            return await PagedList<Grand.Core.Domain.Equipment.Equipment>.Create(query, pageIndex, pageSize);
        }
        public virtual async Task UpdateEquipment(Core.Domain.Equipment.Equipment equipment)
        {
            await _equipmentRepository.UpdateAsync(equipment);
        }
    }
}
