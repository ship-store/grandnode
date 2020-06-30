using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.EquipmentStatus
{
    public class EquipmentStatusService : IEquipmentStatusService
    {
        private readonly IRepository<Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus> _equipmentStatusRepository;
        
        public EquipmentStatusService(IRepository<Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus> _equipmentStatusRepository)
        {
            this._equipmentStatusRepository = _equipmentStatusRepository;
        }
       
        async Task<IPagedList<Core.Domain.EquipmentStatusEntity.EquipmentStatus>> IEquipmentStatusService.GetAllEquipmentStatus(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _equipmentStatusRepository.Table;
         
            return await PagedList< Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.EquipmentStatusEntity.EquipmentStatus>> IEquipmentStatusService.GetAllEquipmentStatusAsList()
        {
            var query = _equipmentStatusRepository.Table;


           

            return await PagedList<Grand.Core.Domain.EquipmentStatusEntity.EquipmentStatus>.Create(query ,0,15);
        }

        Task IEquipmentStatusService.PrepareEquipmentStatusModel(Core.Domain.EquipmentStatusEntity.EquipmentStatus model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertEquipmentStatus(Core.Domain.EquipmentStatusEntity.EquipmentStatus equipmentStatus)
        {


            await _equipmentStatusRepository.InsertAsync(equipmentStatus);


        }

    }
}
