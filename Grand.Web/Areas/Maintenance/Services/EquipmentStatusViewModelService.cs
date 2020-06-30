using Grand.Core;
using Grand.Core.Domain.EquipmentStatusEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobStatusEntity;
using Grand.Core.Domain.JobType;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.EquipmentStatus;
using Grand.Services.EquipmentType;
using Grand.Services.JobStatus;
using Grand.Services.JobType;
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
    public partial class EquipmentStatusViewModelService : IEquipmentStatusViewModelService
    {
        private readonly IEquipmentStatusService _equipmentStatusService;
        public EquipmentStatusViewModelService(IEquipmentStatusService _equipmentStatusService)
        {
            this._equipmentStatusService = _equipmentStatusService;
            
        }
        Task<IPagedList<EquipmentStatus>> IEquipmentStatusViewModelService.GetAllEquipmentStatus(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<EquipmentStatus>> IEquipmentStatusViewModelService.GetAllEquipmentStatusAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IEquipmentStatusViewModelService.PrepareEquipmentStatusModel(EquipmentStatusModel addNewEquipmentStatus, object p, bool v)
        {
            try
            {

                var equipmentStatus = new EquipmentStatus();

                equipmentStatus.Status = addNewEquipmentStatus.Status;
               
                await  _equipmentStatusService.InsertEquipmentStatus(equipmentStatus);
            }
            catch (Exception ex)
            {
                var equipmentStatus = new EquipmentStatus();

                equipmentStatus.Status = addNewEquipmentStatus.Status;

                await _equipmentStatusService.InsertEquipmentStatus(equipmentStatus);

            }
        }
    }
}
