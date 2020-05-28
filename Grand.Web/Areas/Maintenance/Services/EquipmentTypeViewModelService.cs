using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.EquipmentType;
using Grand.Services.Maker;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class EquipmentTypeViewModelService : IEquipmentTypeViewModelService
    {
        private readonly IEquipmentTypeService _equipmentTypeService;
        public EquipmentTypeViewModelService(IEquipmentTypeService _equipmentTypeService)
        {
            this._equipmentTypeService = _equipmentTypeService;
            
        }
        Task<IPagedList<EquipmentType>> IEquipmentTypeViewModelService.GetAllEquipmentTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<EquipmentType>> IEquipmentTypeViewModelService.GetAllEquipmentTypeAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IEquipmentTypeViewModelService.PrepareEquipmentTypeModel(EquipmentTypeModel addNewEquipmentType, object p, bool v)
        {
            try
            {

                var equipmentType = new EquipmentType();

                equipmentType.Equipment_type = addNewEquipmentType.Equipment_type;
               
                await  _equipmentTypeService.InsertEquipmentType(equipmentType);
            }
            catch (Exception ex)
            {
                var equipmentType = new EquipmentType();

                equipmentType.Equipment_type = addNewEquipmentType.Equipment_type;

                await _equipmentTypeService.InsertEquipmentType(equipmentType);

            }
        }
    }
}
