using Grand.Core;
using Grand.Core.Domain.BreakdownJob;
using Grand.Services.BreakdownJob;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class BreakdownJobViewModelService : IBreakdownJobViewModelService
    {
        private readonly IBreakdownJobService _breakdownJobService;



        public BreakdownJobViewModelService(IBreakdownJobService _breakdownJobService)
        {
            this._breakdownJobService = _breakdownJobService;

        }
        Task<IPagedList<BreakdownJob>> IBreakdownJobViewModelService.GetAllBreakdownJobs(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<BreakdownJob>> IBreakdownJobViewModelService.GetAllBreakdownJobsAsList(string id)
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



        async Task IBreakdownJobViewModelService.PrepareBreakdownJobModel(BreakdownJobModel addNewBreakdownJob, object p, bool v)
        {
            try
            {

                var breakdownJob = new BreakdownJob();

                breakdownJob.EquipmentName = addNewBreakdownJob.EquipmentName;
                breakdownJob.JobOrder = addNewBreakdownJob.JobOrder;
                breakdownJob.Title = addNewBreakdownJob.Title;
                breakdownJob.JobReportedDate = addNewBreakdownJob.JobReportedDate;
                breakdownJob.ReportedBy = addNewBreakdownJob.ReportedBy;
                breakdownJob.Status = addNewBreakdownJob.Status;
                //breakdownJob.Hull_no = addNewBreakdownJob.Hull_no;
                //breakdownJob.Auxiliary_Engine = addNewBreakdownJob.Auxiliary_Engine;
                //breakdownJob.Main_Engine = addNewBreakdownJob.Main_Engine;
                await _breakdownJobService.InsertBreakdownJob(breakdownJob);
                // var vessel = addNewVessel.ToEntity();

            }
            catch (Exception ex)
            {
                var breakdownJob = new BreakdownJob();

                breakdownJob.EquipmentName = addNewBreakdownJob.EquipmentName;
                breakdownJob.JobOrder = addNewBreakdownJob.JobOrder;
                breakdownJob.Title = addNewBreakdownJob.Title;
                breakdownJob.JobReportedDate = addNewBreakdownJob.JobReportedDate;
                breakdownJob.ReportedBy = addNewBreakdownJob.ReportedBy;
                breakdownJob.Status = addNewBreakdownJob.Status;
                //breakdownJob.Hull_no = addNewBreakdownJob.Hull_no;
                //breakdownJob.Auxiliary_Engine = addNewBreakdownJob.Auxiliary_Engine;
                //breakdownJob.Main_Engine = addNewBreakdownJob.Main_Engine;
                await _breakdownJobService.InsertBreakdownJob(breakdownJob);

            }
        }
    }
}
