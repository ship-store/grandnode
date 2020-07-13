using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Rank
{
    public class RankService : IRankService
    {
        private readonly IRepository<Grand.Core.Domain.RankEntity.Rank> _rankRepository;
        
        public RankService(IRepository<Grand.Core.Domain.RankEntity.Rank> _rankRepository)
        {
            this._rankRepository = _rankRepository;
        }
       
        async Task<IPagedList<Core.Domain.RankEntity.Rank>> IRankService.GetAllRanks(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _rankRepository.Table;
         
            return await PagedList< Grand.Core.Domain.RankEntity.Rank>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.RankEntity.Rank>> IRankService.GetAllRankAsList()
        {
            var query = _rankRepository.Table;


           

            return await PagedList<Grand.Core.Domain.RankEntity.Rank>.Create(query ,0,15);
        }

        Task IRankService.PrepareRankModel(Core.Domain.RankEntity.Rank model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertRank(Core.Domain.RankEntity.Rank rank)
        {


            await _rankRepository.InsertAsync(rank);


        }
        public virtual async Task UpdateRank(Core.Domain.RankEntity.Rank rank)
        {
            await _rankRepository.UpdateAsync(rank);
        }
        public virtual Task<Core.Domain.RankEntity.Rank> GetRankById(string rankId)
        {
            return _rankRepository.GetByIdAsync(rankId);
        }

    }
}
