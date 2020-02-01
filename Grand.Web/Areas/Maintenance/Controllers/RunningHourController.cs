using Grand.Core.Domain.BreakdownJob;
using Grand.Core.Domain.UnplannedJobs;
using Grand.Core.Domain.Vessel;
using Grand.Framework.Kendoui;
using Grand.Services.UnplannedJobs;
using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Admin.Models.Vendors;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Grand.Services.Equipments;
using Grand.Core.Domain.Equipment;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class RunningHourController : BaseAdminController
    {
        private readonly IEquipmentService _equipmentService;

        //private readonly IRunningHourViewModelService _runninghourViewModelService;
        public RunningHourController( IEquipmentService _equipmentService)
        {
           // this._runninghourViewModelService = _runninghourViewModelService;
            this._equipmentService = _equipmentService;
        }
        // list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> List()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReadRunningHour(DataSourceRequest command, UnplannedJobListModel model)
        {
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            if (VesselName != null)
            {

                var equipments = await _equipmentService.GetAllEquipment(model.SearchName, command.Page - 1, command.PageSize, true);
                List<Equipment> equipmentlist = new List<Equipment>();
                foreach (Equipment item in equipments.Where(x => x.Vessel.ToLower() == VesselName.ToLower()))
                {
                    equipmentlist.Add(item);
                }
                var gridModel = new DataSourceResult { Data = equipmentlist };
                return Json(gridModel);

            }
            else
            {
                return RedirectToAction("List", "RunningHour");
            }

        }

        [HttpPost]
        public async Task<JsonResult> EditItem(DataSourceRequest command, List<EquipmentModel> models)
        {
            foreach (var item in models)
            {

                var vessel = await _equipmentService.GetEquipmentById(item.Id);

                vessel.Sub1_description = item.Sub1_description;
                vessel.Sub1_number = item.Sub1_number;
                vessel.Sub2_description = item.Sub2_description;
                vessel.Sub2_number = item.Sub2_number;
                vessel.Reading = item.Reading;


                //vessel.JobOrder = item.JobOrder;

                //vessel.Title = item.Title;
                //vessel.JobReportedDate = item.JobReportedDate;
                //vessel.ReportedBy = item.ReportedBy;
                //vessel.Status = item.Status;


                await _equipmentService.UpdateEquipment(vessel);
            }
            return Json(models);
        }

        //[HttpGet]
        //public async Task<IActionResult> AddRunningHour()
        //{
        //    var model = await Task.FromResult<object>(null);

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddRunningHourDetails(RunningHourModel addRunningHour)
        //{
        //    await _runninghourViewModelService.PrepareRunningHourModel(addRunningHour, "RunningHour", true);
        //    return RedirectToAction("List", "RunningHour");
        //}

        //[HttpGet]
        //public async Task<IActionResult> AddEquipment()
        //{
        //    var model = await Task.FromResult<object>(null);

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddEquipmentDetails(RunningHourModel addRunningHour)
        //{
        //    await _runninghourViewModelService.PrepareRunningHourModel(addRunningHour, "RunningHour", true);
        //    return RedirectToAction("List", "RunningHour");
        //}
    }
}
