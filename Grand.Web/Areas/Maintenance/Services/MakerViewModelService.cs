using Grand.Core;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Maker;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class MakerViewModelService : IMakerViewModelService
    {
        private readonly IMakerService _makerService;
        
        
    
        public MakerViewModelService(IMakerService _makerService)
        {
            this._makerService = _makerService;
            
        }
        Task<IPagedList<Maker>> IMakerViewModelService.GetAllMakers(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Maker>> IMakerViewModelService.GetAllMakersAsList(string id)
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

      

        async Task IMakerViewModelService.PrepareMakerModel(MakerModel addNewMaker, object p, bool v)
        {
            try
            {

                var maker = new Maker();

                maker.Name = addNewMaker.Name;
                maker.Code = addNewMaker.Code;
                maker.Country = addNewMaker.Country;
                
                await  _makerService.InsertMaker(maker);
                // var vessel = addNewVessel.ToEntity();

            }
            catch (Exception ex)
            {
                var maker = new Maker();
                maker.Name = addNewMaker.Name;
                maker.Code = addNewMaker.Code;
                maker.Country = addNewMaker.Country;

                await _makerService.InsertMaker(maker);

            }
        }
    }
}
