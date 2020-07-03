using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.RankEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Frequency;
using Grand.Services.Maker;
using Grand.Services.Rank;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class RankViewModelService : IRankViewModelService
    {
        private readonly IRankService _rankService;
        private readonly IRepository<Rank> _RankRepository;

        public RankViewModelService(IRankService _rankService,
            IRepository<Rank> _RankRepository)
        {
            this._rankService = _rankService;
            this._RankRepository = _RankRepository;

        }
        Task<IPagedList<Rank>> IRankViewModelService.GetAllRanks(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<Rank>> IRankViewModelService.GetAllRankAsList(string id)
        {
            await Task.FromResult(0);

            var query = _RankRepository.Table;

           var result=await PagedList<Rank>.Create(query, 0,15);

            return result;
        }
        async Task IRankViewModelService.PrepareRankModel(RankModel addNewRank, object p, bool v)
        {
            try
            {

                var rank = new Rank();

                rank.Ranks = addNewRank.Ranks;
               
                await _rankService.InsertRank(rank);
            }
            catch (Exception ex)
            {
                var rank = new Rank();

                rank.Ranks = addNewRank.Ranks;

                await _rankService.InsertRank(rank);
            }
        }

    }
}
