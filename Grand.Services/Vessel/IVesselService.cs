using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.Vessel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Vessel
{
    public interface IVesselService
    {
         Task<IPagedList<Grand.Core.Domain.Vessel.Vessel>> GetAllVessels(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.Vessel.Vessel>> GetAllVesselAsList();
           Task PrepareVesselModel(Grand.Core.Domain.Vessel.Vessel model1, object p, bool v);
       
        Task InsertVessel(Core.Domain.Vessel.Vessel vessel);
      //  Task<Grand.Core.Domain.Vessel.Vessel> GetVesselById(string Ids);
        Task<IList<Grand.Core.Domain.Vessel.Vessel>> GetVesselByIds(string[] vesselIds);
        Task DeleteVessel(Grand.Core.Domain.Vessel.Vessel vessel);
        Task<Core.Domain.Vessel.Vessel> GetVesselById(string Id);
        Task UpdateVessel(Grand.Core.Domain.Vessel.Vessel vessel);

    }
}
