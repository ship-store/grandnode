using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Cbm
{
    public class CbmService : ICbmService
    {
        private readonly IRepository<Grand.Core.Domain.CbmEntity.CBM> _CbmRepository;

        public CbmService(IRepository<Grand.Core.Domain.CbmEntity.CBM> _CBMRepository)
        {
            this._CbmRepository = _CBMRepository;
        }



        async Task<IPagedList<Core.Domain.CbmEntity.CBM>> ICbmService.GetAllCbm(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _CbmRepository.Table;
            return await PagedList< Grand.Core.Domain.CbmEntity.CBM>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.CbmEntity.CBM>> ICbmService.GetAllCbmAsList()
        {
            var query = _CbmRepository.Table; 
            return await PagedList<Grand.Core.Domain.CbmEntity.CBM>.Create(query ,0,15);
        }

        Task ICbmService.PrepareCbmModel(Core.Domain.CbmEntity.CBM model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertCbm(Core.Domain.CbmEntity.CBM cbm)
        {
            await _CbmRepository.InsertAsync(cbm);
        }
        public virtual async Task UpdateCbm(Core.Domain.CbmEntity.CBM cbm)
        {
            await _CbmRepository.UpdateAsync(cbm);
        }
        public virtual Task<Core.Domain.CbmEntity.CBM> GetCbmById(string cbm)
        {
            return _CbmRepository.GetByIdAsync(cbm);
        }

    }
}
