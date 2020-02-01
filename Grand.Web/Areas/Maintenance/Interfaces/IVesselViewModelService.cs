using Grand.Core;
using Grand.Core.Domain.Vessel;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IVesselViewModelService
    {
        Task<IPagedList<Vessel>> GetAllVessels(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<Vessel>> GetAllVesselsAsList(string id);
        Task PrepareVesselModel(VesselModel model);
        Task DeleteVessel(Vessel vessel);
        Task DeleteSelected(IList<string> selectedIds);
    }
}
