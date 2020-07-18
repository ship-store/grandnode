using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Maker
{
    public class MakerService : IMakerService
    {
        private readonly IRepository<Grand.Core.Domain.MakerEntity.Maker> _makerRepository;
        
        public MakerService(IRepository<Grand.Core.Domain.MakerEntity.Maker> _makerRepository)
        {
            this._makerRepository = _makerRepository;
        }
       
        async Task<IPagedList<Core.Domain.MakerEntity.Maker>> IMakerService.GetAllMakers(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _makerRepository.Table;
         
            return await PagedList< Grand.Core.Domain.MakerEntity.Maker>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.MakerEntity.Maker>> IMakerService.GetAllMakerAsList()
        {
            var query = _makerRepository.Table;


           

            return await PagedList<Grand.Core.Domain.MakerEntity.Maker>.Create(query ,0,15);
        }

        Task IMakerService.PrepareMakerModel(Core.Domain.MakerEntity.Maker model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertMaker(Core.Domain.MakerEntity.Maker maker)
        {


            await _makerRepository.InsertAsync(maker);


        }
        public virtual Task<Core.Domain.MakerEntity.Maker> GetMakerById(string makerId)
        {
            return _makerRepository.GetByIdAsync(makerId);
        }
        public virtual async Task UpdateMaker(Core.Domain.MakerEntity.Maker maker)
        {
            await _makerRepository.UpdateAsync(maker);
        }


    }
}
