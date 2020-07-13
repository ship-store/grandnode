using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.SafetyLevel
{
    public class SafetyLevelService : ISafetyLevelService
    {
        private readonly IRepository<Grand.Core.Domain.SafetyLevelEntity.SafetyLevel> _safetyLevelRepository;
        
        public SafetyLevelService(IRepository<Grand.Core.Domain.SafetyLevelEntity.SafetyLevel> _safetyLevelRepository)
        {
            this._safetyLevelRepository = _safetyLevelRepository;
        }
       
        async Task<IPagedList<Core.Domain.SafetyLevelEntity.SafetyLevel>> ISafetyLevelService.GetAllSafetyLevels(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _safetyLevelRepository.Table;
         
            return await PagedList< Grand.Core.Domain.SafetyLevelEntity.SafetyLevel>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.SafetyLevelEntity.SafetyLevel>> ISafetyLevelService.GetAllSafetyLevelAsList()
        {
            var query = _safetyLevelRepository.Table;


           

            return await PagedList<Grand.Core.Domain.SafetyLevelEntity.SafetyLevel>.Create(query ,0,15);
        }

        Task ISafetyLevelService.PrepareSafetyLevelModel(Core.Domain.SafetyLevelEntity.SafetyLevel model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertSafetyLevel(Core.Domain.SafetyLevelEntity.SafetyLevel safetyLevel)
        {


            await _safetyLevelRepository.InsertAsync(safetyLevel);


        }
        public virtual Task<Core.Domain.SafetyLevelEntity.SafetyLevel> GetSafetyLevelById(string safetyLevelId)
        {
            return _safetyLevelRepository.GetByIdAsync(safetyLevelId);
        }
        public virtual async Task UpdateSafetyLevel(Core.Domain.SafetyLevelEntity.SafetyLevel safetyLevel)
        {
            await _safetyLevelRepository.UpdateAsync(safetyLevel);
        }

    }
}
