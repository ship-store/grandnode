using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Rank
{
    public interface IRankService
    {
         Task<IPagedList<Grand.Core.Domain.RankEntity.Rank>> GetAllRanks(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.RankEntity.Rank>> GetAllRankAsList();
           Task PrepareRankModel(Grand.Core.Domain.RankEntity.Rank model1, object p, bool v);
       
        Task InsertRank(Core.Domain.RankEntity.Rank rank);
    }
}
