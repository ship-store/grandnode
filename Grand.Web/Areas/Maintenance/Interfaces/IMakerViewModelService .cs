using Grand.Core;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IMakerViewModelService
    {
        Task<IPagedList<Maker>> GetAllMakers(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Task<IPagedList<Maker>> GetAllMakersAsList(string id);
        //Task PrepareMakerModel(MakerModel addNewMaker, string v1, bool v2);

        Task PrepareMakerModel(MakerModel model1, object p, bool v);


    }
}
