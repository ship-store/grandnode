using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.PriorityEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Frequency;
using Grand.Services.Maker;
using Grand.Services.Priority;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class PriorityViewModelService : IPriorityViewModelService
    {
        private readonly IPriorityService _priorityService;
        private readonly IRepository<Priority> _PriorityRepository;

        public PriorityViewModelService(IPriorityService _priorityService,
            IRepository<Priority> _PriorityRepository)
        {
            this._priorityService = _priorityService;
            this._PriorityRepository = _PriorityRepository;

        }
        Task<IPagedList<Priority>> IPriorityViewModelService.GetAllPriorities(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<Priority>> IPriorityViewModelService.GetAllPriorityAsList(string id)
        {
            await Task.FromResult(0);

            var query = _PriorityRepository.Table;

           var result=await PagedList<Priority>.Create(query, 0,15);

            return result;
        }
        async Task IPriorityViewModelService.PreparePriorityModel(PriorityModel addNewPriority, object p, bool v)
        {
            try
            {

                var priority = new Priority();

                priority.Priorities = addNewPriority.Priorities;
               
                await _priorityService.InsertPriority(priority);
            }
            catch (Exception ex)
            {
                var priority = new Priority();

                priority.Priorities = addNewPriority.Priorities;

                await _priorityService.InsertPriority(priority);
            }
        }

    }
}
