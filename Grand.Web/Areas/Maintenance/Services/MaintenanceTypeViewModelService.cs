using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.FrequencyTypeEntity;
using Grand.Core.Domain.MaintenanceTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Frequency;
using Grand.Services.FrequencyType;
using Grand.Services.MaintenanceType;
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
    public partial class MaintenanceTypeViewModelService : IMaintenanceTypeViewModelService
    {
        private readonly IMaintenanceTypeService _maintenanceTypeService;
        private readonly IRepository<MaintenanceType> _MaintenanceTypeRepository;

        public MaintenanceTypeViewModelService(IMaintenanceTypeService _maintenanceTypeService,
            IRepository<MaintenanceType> _MaintenanceTypeRepository)
        {
            this._maintenanceTypeService = _maintenanceTypeService;
            this._MaintenanceTypeRepository = _MaintenanceTypeRepository;

        }
        Task<IPagedList<MaintenanceType>> IMaintenanceTypeViewModelService.GetAllMaintenanceTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<MaintenanceType>> IMaintenanceTypeViewModelService.GetAllMaintenanceTypeAsList(string id)
        {
            await Task.FromResult(0);

            var query = _MaintenanceTypeRepository.Table;

           var result=await PagedList<MaintenanceType>.Create(query, 0,15);

            return result;
        }
        async Task IMaintenanceTypeViewModelService.PrepareMaintenanceTypeModel(MaintenanceTypeModel addNewMaintenanceType, object p, bool v)
        {
            try
            {

                var maintenanceType = new MaintenanceType();

                maintenanceType.Maintenance_type = addNewMaintenanceType.Maintenance_type;
               
                await _maintenanceTypeService.InsertMaintenanceType(maintenanceType);
            }
            catch (Exception ex)
            {
                var maintenanceType = new MaintenanceType();

                maintenanceType.Maintenance_type = addNewMaintenanceType.Maintenance_type;

                await _maintenanceTypeService.InsertMaintenanceType(maintenanceType);
            }
        }

    }
}
