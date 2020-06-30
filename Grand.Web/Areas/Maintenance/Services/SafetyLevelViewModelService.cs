using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.SafetyLevelEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Maker;
using Grand.Services.SafetyLevel;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class SafetyLevelViewModelService : ISafetyLevelViewModelService
    {
        private readonly ISafetyLevelService _safetyLevelService;
        private readonly IRepository<SafetyLevel> _SafetyLevelRepository;

        public SafetyLevelViewModelService(ISafetyLevelService _safetyLevelService,
            IRepository<SafetyLevel> _SafetyLevelRepository)
        {
            this._safetyLevelService = _safetyLevelService;
            this._SafetyLevelRepository = _SafetyLevelRepository;

        }
        Task<IPagedList<SafetyLevel>> ISafetyLevelViewModelService.GetAllSafetyLevels(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<SafetyLevel>> ISafetyLevelViewModelService.GetAllSafetyLevelAsList(string id)
        {
            await Task.FromResult(0);

            var query = _SafetyLevelRepository.Table;

           var result=await PagedList<SafetyLevel>.Create(query, 0,15);

            return result;
        }
        async Task ISafetyLevelViewModelService.PrepareSafetyLevelModel(SafetyLevelModel addNewSafetyLevel, object p, bool v)
        {
            try
            {

                var safetyLevel = new SafetyLevel();

                safetyLevel.Safety_level = addNewSafetyLevel.Safety_level;
               
                await _safetyLevelService.InsertSafetyLevel(safetyLevel);
            }
            catch (Exception ex)
            {
                var safetyLevel = new SafetyLevel();

                safetyLevel.Safety_level = addNewSafetyLevel.Safety_level;

                await _safetyLevelService.InsertSafetyLevel(safetyLevel);

            }
        }

        //public Task<IPagedList<Department>> GetAllDepartments(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
