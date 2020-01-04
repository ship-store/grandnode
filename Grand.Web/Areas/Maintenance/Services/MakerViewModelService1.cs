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
    public partial class MakerViewModelService1 : IMakerViewModelService1
    {
        private readonly IMakerService1 _makerService;
        
        
    
        public MakerViewModelService1(IMakerService1 _makerService)
        {
            this._makerService = _makerService;
            
        }
        Task<IPagedList<Maker1>> IMakerViewModelService1.GetAllMakers(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Maker1>> IMakerViewModelService1.GetAllMakersAsList(string id)
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

      

        async Task IMakerViewModelService1.PrepareMakerModel(MakerModel1 addNewMaker, object p, bool v)
        {
            try
            {

                var maker = new Maker1();

                maker.Maker = addNewMaker.Maker;
                maker.Model = addNewMaker.Model;
                maker.Remark = addNewMaker.Remark;
              
                await  _makerService.InsertMaker(maker);
                // var vessel = addNewVessel.ToEntity();

            }
            catch (Exception )
            {
                var maker = new Maker1();
                maker.Maker = addNewMaker.Maker;
                maker.Model = addNewMaker.Model;
                maker.Remark = addNewMaker.Remark;

                await _makerService.InsertMaker(maker);

            }
        }
    }
}
