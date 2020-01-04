using Grand.Core;
using Grand.Core.Domain.BreakdownJob;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IBreakdownJobViewModelService
    {
        Task<IPagedList<BreakdownJob>> GetAllBreakdownJobs(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Task<IPagedList<BreakdownJob>> GetAllBreakdownJobsAsList(string id);
        //Task PrepareVesselModel(VesselModel addNewVessel, string v1, bool v2);

        Task PrepareBreakdownJobModel(BreakdownJobModel model1, object p, bool v);


    }
}
