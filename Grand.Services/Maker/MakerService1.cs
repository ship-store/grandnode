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
    public class MakerService1 : IMakerService1
    {
        private readonly IRepository<Grand.Core.Domain.MakerEntity.Maker1> _makerRepository;
        
        public MakerService1(IRepository<Grand.Core.Domain.MakerEntity.Maker1> _makerRepository)
        {
            this._makerRepository = _makerRepository;
        }
       
        async Task<IPagedList<Core.Domain.MakerEntity.Maker1>> IMakerService1.GetAllMakers(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _makerRepository.Table;
         
            return await PagedList< Grand.Core.Domain.MakerEntity.Maker1>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.MakerEntity.Maker1>> IMakerService1.GetAllMakerAsList()
        {
            var query = _makerRepository.Table;


           

            return await PagedList<Grand.Core.Domain.MakerEntity.Maker1>.Create(query ,0,15);
        }

        Task IMakerService1.PrepareMakerModel(Core.Domain.MakerEntity.Maker1 model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertMaker(Core.Domain.MakerEntity.Maker1 maker)
        {


            await _makerRepository.InsertAsync(maker);


        }

    }
}
