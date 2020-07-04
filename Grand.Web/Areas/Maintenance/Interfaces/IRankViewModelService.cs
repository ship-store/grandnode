using Grand.Core;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.RankEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IRankViewModelService
    {
        Task<IPagedList<Rank>> GetAllRanks(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<Rank>> GetAllRankAsList(string id);
        
        Task PrepareRankModel(RankModel model1, object p, bool v);
    }
}
