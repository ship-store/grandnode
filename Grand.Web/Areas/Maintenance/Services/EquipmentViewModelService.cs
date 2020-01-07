using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Services.Equipments;
using Grand.Core.Domain.Equipment;

namespace Grand.Web.Areas.Maintenance.Services
{
    public class EquipmentViewModelService: IEquipmentViewModelService
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentViewModelService(IEquipmentService _equipmentService)
        {
            this._equipmentService = _equipmentService;
        }
        async Task IEquipmentViewModelService.PrepareEquipmentModel(EquipmentModel equipmentModel, object p, bool v)
        {
            try
            {
                var equipment = new Equipment();
                equipment.Sub1_number = equipmentModel.Sub1_number;
                equipment.Sub1_description = equipmentModel.Sub1_description;
                equipment.Sub2_number = equipmentModel.Sub2_number;
                equipment.Sub2_description = equipmentModel.Sub2_description;
                equipment.Sub3_number = equipmentModel.Sub3_number;
                equipment.Sub3_description = equipmentModel.Sub3_description;
              
            }
            catch (Exception ex)
            {
            }
        }
    }
}
