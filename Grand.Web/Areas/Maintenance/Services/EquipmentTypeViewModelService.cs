using Grand.Core;
using Grand.Core.Data;
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
        private readonly IRepository<EquipmentType> _EquipmentTypeRepository;

        public EquipmentTypeViewModelService(IEquipmentTypeService _equipmentTypeService,
            IRepository<EquipmentType> _EquipmentTypeRepository)
        {
            this._equipmentTypeService = _equipmentTypeService;
            this._EquipmentTypeRepository = _EquipmentTypeRepository;

        }
        Task<IPagedList<EquipmentType>> IEquipmentTypeViewModelService.GetAllEquipmentTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

     async Task<IPagedList<EquipmentType>> IEquipmentTypeViewModelService.GetAllEquipmentTypeAsList(string id)
        {
            await Task.FromResult(0);

            var query = _EquipmentTypeRepository.Table;

           var result=await PagedList<EquipmentType>.Create(query, 0,15);

            return result;
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
