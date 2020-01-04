using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grand.Core.Data;


namespace Grand.Services.Equipments
{
    public  class EquipmentService: IEquipmentService
    {
        private readonly IRepository<Grand.Core.Domain.Equipment.Equipment> _equipmentRepository;
        public EquipmentService(IRepository<Grand.Core.Domain.Equipment.Equipment> _equipmentRepository)
        {
            this._equipmentRepository = _equipmentRepository;
        }
       
        public virtual async Task InsertEquipment(Grand.Core.Domain.Equipment.Equipment equipment)
        {
            await _equipmentRepository.InsertAsync(equipment);

        }
    }
}
