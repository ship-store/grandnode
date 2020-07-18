using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.MaintenanceType
{
    public class MaintenanceTypeService : IMaintenanceTypeService
    {
        private readonly IRepository<Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType> _maintenanceTypeRepository;
        
        public MaintenanceTypeService(IRepository<Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType> _maintenanceTypeRepository)
        {
            this._maintenanceTypeRepository = _maintenanceTypeRepository;
        }
       
        async Task<IPagedList<Core.Domain.MaintenanceTypeEntity.MaintenanceType>> IMaintenanceTypeService.GetAllMaintenanceTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _maintenanceTypeRepository.Table;
         
            return await PagedList< Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.MaintenanceTypeEntity.MaintenanceType>> IMaintenanceTypeService.GetAllMaintenanceTypeAsList()
        {
            var query = _maintenanceTypeRepository.Table;


           

            return await PagedList<Grand.Core.Domain.MaintenanceTypeEntity.MaintenanceType>.Create(query ,0,15);
        }

        Task IMaintenanceTypeService.PrepareMaintenanceTypeModel(Core.Domain.MaintenanceTypeEntity.MaintenanceType model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertMaintenanceType(Core.Domain.MaintenanceTypeEntity.MaintenanceType maintenanceType)
        {


            await _maintenanceTypeRepository.InsertAsync(maintenanceType);


        }
        public virtual Task<Core.Domain.MaintenanceTypeEntity.MaintenanceType> GetMaintenanceTypeById(string maintenanceTypeId)
        {
            return _maintenanceTypeRepository.GetByIdAsync(maintenanceTypeId);
        }
        public virtual async Task UpdateMaintenanceType(Core.Domain.MaintenanceTypeEntity.MaintenanceType maintenanceType)
        {
            await _maintenanceTypeRepository.UpdateAsync(maintenanceType);
        }

    }
}
