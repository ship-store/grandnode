using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.CriticalEntity;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Critical;
using Grand.Services.Department;
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
    public partial class CriticalViewModelService : ICriticalViewModelService
    {
        private readonly ICriticalService _criticalService;
        private readonly IRepository<Critical> _CriticalRepository;

        public CriticalViewModelService(ICriticalService _criticalService,
            IRepository<Critical> _CriticalRepository)
        {
            this._criticalService = _criticalService;
            this._CriticalRepository = _CriticalRepository;

        }
        Task<IPagedList<Critical>> ICriticalViewModelService.GetAllCriticals(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<Critical>> ICriticalViewModelService.GetAllCriticalAsList(string id)
        {
            await Task.FromResult(0);

            var query = _CriticalRepository.Table;

           var result=await PagedList<Critical>.Create(query, 0,15);

            return result;
        }
        async Task ICriticalViewModelService.PrepareCriticalModel(CriticalModel addNewCritical, object p, bool v)
        {
            try
            {

                var critical = new Critical();

                critical.Criticals = addNewCritical.Criticals;
               
                await  _criticalService.InsertCritical(critical);
            }
            catch (Exception ex)
            {
                var critical = new Critical();

                critical.Criticals = addNewCritical.Criticals;

                await _criticalService.InsertCritical(critical);

            }
        }
    }
}
