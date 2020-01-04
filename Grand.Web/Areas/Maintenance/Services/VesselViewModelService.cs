using Grand.Core;
using Grand.Core.Domain.Vessel;
using Grand.Services.Vessel;

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
            
          
            //for (var i = 0; i < vessels.Count; i++)
            //{
            //    var vessel = vessels[i];

            //    await _vesselService.DeleteVessel(vessel);
            //}
        }


        public async Task DeleteVessel(Vessel vessel)
        {
            await _vesselService.DeleteVessel(vessel);
            //activity log
            //await _customerActivityService.InsertActivity("DeleteProduct", product.Id, _localizationService.GetResource("ActivityLog.DeleteProduct"), product.Name);
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




      
        //public virtual  async Task PrepareVesselModel(VesselModel addNewVessel, object v1, bool v2)
        //{
        //    try
        //    {

        //        var vessel = new Vessel();

        //        vessel.Vessel_name = addNewVessel.Vessel_name;
        //        vessel.Vessel_type = addNewVessel.Vessel_type;
        //        vessel.IMO = addNewVessel.IMO;
        //        vessel.Shipyard = addNewVessel.Shipyard;
        //        vessel.Flag = addNewVessel.Flag;
        //        vessel.Class = addNewVessel.Class;
        //        vessel.Hull_no = addNewVessel.Hull_no;
        //        vessel.Auxiliary_Engine = addNewVessel.Auxiliary_Engine;
        //        vessel.Main_Engine = addNewVessel.Main_Engine;
        //        await _ivesselService.InsertVessel(vessel);
        //        // var vessel = addNewVessel.ToEntity();

        //    }
        //    catch (Exception ex)
        //    {
        //        var vessel = new Vessel();
        //        vessel.Vessel_name = addNewVessel.Vessel_name;
        //        vessel.Vessel_type = addNewVessel.Vessel_type;
        //        vessel.IMO = addNewVessel.IMO;
        //        vessel.Shipyard = addNewVessel.Shipyard;
        //        vessel.Flag = addNewVessel.Flag;
        //        vessel.Class = addNewVessel.Class;
        //        vessel.Hull_no = addNewVessel.Hull_no;
        //        vessel.Auxiliary_Engine = addNewVessel.Auxiliary_Engine;
        //        vessel.Main_Engine = addNewVessel.Main_Engine;
        //        await _ivesselService.InsertVessel(vessel);

        //    }
        //}

      

        async Task IVesselViewModelService.PrepareVesselModel(VesselModel addNewVessel, object p, bool v)
        {
            try
            {

                var vessel = new Vessel();

                vessel.Vessel_name = addNewVessel.Vessel_name;
                vessel.Vessel_type = addNewVessel.Vessel_type;
                vessel.IMO = addNewVessel.IMO;
                vessel.Shipyard = addNewVessel.Shipyard;
                vessel.Flag = addNewVessel.Flag;
                vessel.Class = addNewVessel.Class;
                vessel.Hull_no = addNewVessel.Hull_no;
                vessel.Auxiliary_Engine = addNewVessel.Auxiliary_Engine;
                vessel.Main_Engine = addNewVessel.Main_Engine;
                await  _vesselService.InsertVessel(vessel);
                // var vessel = addNewVessel.ToEntity();

            }
            catch (Exception )
            {
                var vessel = new Vessel();
                vessel.Vessel_name = addNewVessel.Vessel_name;
                vessel.Vessel_type = addNewVessel.Vessel_type;
                vessel.IMO = addNewVessel.IMO;
                vessel.Shipyard = addNewVessel.Shipyard;
                vessel.Flag = addNewVessel.Flag;
                vessel.Class = addNewVessel.Class;
                vessel.Hull_no = addNewVessel.Hull_no;
                vessel.Auxiliary_Engine = addNewVessel.Auxiliary_Engine;
                vessel.Main_Engine = addNewVessel.Main_Engine;
                await _vesselService.InsertVessel(vessel);

            }
        }
    }
}
