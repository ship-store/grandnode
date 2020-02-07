using Grand.Core.Domain.BreakdownJob;

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
using Grand.Core.Domain.Equipment;
using Grand.Services.Equipments;
using System;
using Grand.Services.BreakdownJob;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class BreakdownJobController : BaseAdminController
    {
        private readonly IBreakdownJobService _breakdownJobService;
        private readonly IEquipmentService _equipmentService;

        private readonly IBreakdownJobViewModelService _breakdownJobViewModelService;
        public BreakdownJobController(IEquipmentService _equipmentService, IBreakdownJobViewModelService _breakdownJobViewModelService, IBreakdownJobService _breakdownJobService)
        {
            this._breakdownJobViewModelService = _breakdownJobViewModelService;
            this._breakdownJobService = _breakdownJobService;
            this._equipmentService = _equipmentService;
        }
        // list
        public IActionResult Index() => RedirectToAction("List");

        public async Task<IActionResult> List()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> EditItem2(string Bid, string remark)
        {
            DateTime today = DateTime.Today;
            var id = Bid;
            var vessel = await _breakdownJobService.GetBreakdownJobById(id);

            vessel.JobCompletedDate = today.ToString("yyyy-MM-dd");
            vessel.Status = "Completed";
            vessel.Remark = remark;

            await _breakdownJobService.UpdateBreakdownJob(vessel);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> ReadData(DataSourceRequest command, BreakdownJobListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (VesselName != null)
                {

                    var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs(model.SearchName, command.Page - 1, command.PageSize, true);
                    List<BreakdownJob> breakdownlist = new List<BreakdownJob>();
                    foreach (BreakdownJob item in breakdownJobs.Where(x => x.Vessel == VesselName))
                    {
                        breakdownlist.Add(item);
                    }
                    var gridModel = new DataSourceResult { Data = breakdownlist.ToList().Where(x => x.DeleteStatus != "1") };
                    return Json(gridModel);

                }
                else
                {
                    return RedirectToAction("Success", "Register");
                }
            }
            catch (System.Exception)
            {

                return RedirectToAction("Success", "Register");
            }


        }

                return RedirectToAction("Success", "Register");
            }

        }


        //[HttpPost]
        //public async Task<IActionResult> ReadData(DataSourceRequest command, BreakdownJobListModel model, string id)
        //{
        //    try
        //    {
        //        var VesselName = HttpContext.Session.GetString("VesselName").ToString();
        //        var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs(model.SearchName, command.Page - 1, command.PageSize, true);
        //        List<BreakdownJob> breakdownlist = new List<BreakdownJob>();
        //        foreach (BreakdownJob item in breakdownJobs.Where(x => x.Vessel == VesselName))
        //        {
        //            breakdownlist.Add(item);
        //        }
        //        var gridModel = new DataSourceResult { Data = breakdownlist.ToList().Where(x => x.DeleteStatus != "1") };
        //        return Json(gridModel);
        //    }
        //    catch (System.Exception)
        //    {

        //        return RedirectToAction("Success", "Register");
        //    }

        //}


        [HttpGet]
        public async Task<IActionResult> AddBreakdownJob()
        {
            var model = await Task.FromResult<object>(null);
            var VesselName = HttpContext.Session.GetString("VesselName").ToString().ToLower();
            var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs("", 0, 500, true);
            List<BreakdownJob> breakdownlist = new List<BreakdownJob>();
            int max = 9999;

            var VesselName = HttpContext.Session.GetString("VesselName").ToString().ToLower();
            var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs("", 0, 500, true);
            List<BreakdownJob> unplannedlist = new List<BreakdownJob>();
            int max = 9999;
            foreach (BreakdownJob item in breakdownJobs)
            {

                if (item.JobOrder > max)
                {
                    max = item.JobOrder;
                }
            }
            int order = max + 1;
            ViewBag.joborder = VesselName + order;
            var equipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
            List<Equipment> equipmentList = new List<Equipment>();
            foreach (Equipment item in equipments.Where(x => x.Vessel == VesselName))
            {
                equipmentList.Add(item);
            }
            ViewBag.MyList = equipmentList;

            return View(equipmentList);
        }

            return View(equipmentList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBreakdownJobDetails(BreakdownJobModel addNewBreakdownJob)

        {

            var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs("", 0, 500, true);
            List<BreakdownJob> breakdownlist = new List<BreakdownJob>();
            int max = 9999;
            string VesselName = HttpContext.Session.GetString("VesselName").ToString();
            foreach (BreakdownJob item in breakdownJobs)
            {


                if (item.JobOrder > max)
                {
                    max = item.JobOrder;
                }
            }
            addNewBreakdownJob.JobOrder = max + 1;
            await _breakdownJobViewModelService.PrepareBreakdownJobModel(addNewBreakdownJob, "Vishnu", true);
            return RedirectToAction("List", "breakdownJob");
        }
        public async Task<IActionResult> Edit(string id)
        {
            var breakdownjob = await _breakdownJobService.GetBreakdownJobById(id);
            BreakdownJobDisplayModel display = new BreakdownJobDisplayModel { BreakdownJobID = breakdownjob.Id, EquipmentName = breakdownjob.EquipmentName, JobOrder = breakdownjob.JobOrder, Title = breakdownjob.Title, JobReportedDate = breakdownjob.JobReportedDate, ReportedBy = breakdownjob.ReportedBy, Status = breakdownjob.Status };
            if (breakdownjob == null)
                return RedirectToAction("List");
            return View(display);
        }

        [HttpPost]
        public async Task<JsonResult> EditItem(DataSourceRequest command, List<BreakdownJob> models)
        {
            foreach (var item in models)
            {
                var breakdownJob = await _breakdownJobService.GetBreakdownJobById(item.Id);
                breakdownJob.EquipmentName = item.EquipmentName;
                breakdownJob.JobOrder = item.JobOrder;
                breakdownJob.Title = item.Title;
                breakdownJob.JobReportedDate = item.JobReportedDate;
                breakdownJob.ReportedBy = item.ReportedBy;
                breakdownJob.Status = item.Status;

                await _breakdownJobService.UpdateBreakdownJob(breakdownJob);
            }
            return Json(models);
        }



        [HttpGet]
        public async Task<IActionResult> DeleteSelected(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedbreakdownJob = await _breakdownJobService.GetBreakdownJobById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedbreakdownJob.DeleteStatus = "1";//changin job to postponed
                    await _breakdownJobService.UpdateBreakdownJob(selectedbreakdownJob);
                }
            }

            return Json(new { Result = true });
        }
    }
}
