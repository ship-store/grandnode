using Grand.Core;
using Grand.Core.Domain.Vessel;
using Grand.Services.Vessel;
using Grand.Web.Areas.Admin.Extensions;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class VesselViewModelService : IVesselViewModelService
    {
        private readonly IVesselService _vesselService;
        public VesselViewModelService(IVesselService _vesselService)
        {
            this._vesselService = _vesselService;
            
        }

        public virtual async Task DeleteSelected(IList<string> selectedIds)
        {
            List<Vessel> vessels = new List<Vessel>();
         
           vessels.AddRange(await _vesselService.GetVesselByIds(selectedIds.ToArray()));
        }


        public async Task DeleteVessel(Vessel vessel)
        {
            await _vesselService.DeleteVessel(vessel);
        }

        public Task<IList<Vessel>> GetVesselByIds(string[] vesselIds)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Vessel>> IVesselViewModelService.GetAllVessels(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Vessel>> IVesselViewModelService.GetAllVesselsAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IVesselViewModelService.PrepareVesselModel(VesselModel vesselModel)
        {                
               var vessel= vesselModel.ToEntity();        
                vessel.ActiveStatus = 1;
                await  _vesselService.InsertVessel(vessel);
        }
    }
}
