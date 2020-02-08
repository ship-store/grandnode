﻿using Grand.Core.Domain.BreakdownJob;
using Grand.Core.Domain.Vessel;
using Grand.Framework.Kendoui;
using Grand.Services.BreakdownJob;
using Grand.Services.Vendors;
using Grand.Services.Vessel;
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
using Grand.Services.Report;
using Grand.Core.Domain.BreakdownJobReport;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class BreakdownJobController : BaseAdminController
    {
        private readonly IBreakdownJobService _breakdownJobService;
        private readonly IEquipmentService _equipmentService;
        private readonly IReportViewModelService _reportViewModelService;
        private readonly IReportService _reportService;

        private readonly IBreakdownJobViewModelService _breakdownJobViewModelService;

        public BreakdownJobController(IEquipmentService _equipmentService, IBreakdownJobViewModelService _breakdownJobViewModelService, IBreakdownJobService _breakdownJobService, IReportViewModelService _reportViewModelService, IReportService _reportService)
        {

            this._breakdownJobViewModelService = _breakdownJobViewModelService;
            this._breakdownJobService = _breakdownJobService;
            this._equipmentService = _equipmentService;
            this._reportViewModelService = _reportViewModelService;
            this._reportService = _reportService;



        }

        // list
        public IActionResult Index() => RedirectToAction("List");

        public async Task<IActionResult> List()
        {

            return View();

        }
        public async Task<IActionResult> List1()
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
            BreakdownJobReportModel output = new BreakdownJobReportModel() {
                EquipmentName = vessel.EquipmentName,

                JobOrder = vessel.JobOrder,
                Title = vessel.Title,
                JobReportedDate = vessel.JobReportedDate,
                ReportedBy = vessel.ReportedBy,
                Status = vessel.Status,

                Vessel = vessel.Vessel,
                DeleteStatus = vessel.DeleteStatus,
                JobCompletedDate = vessel.JobCompletedDate,
                Remark = vessel.Remark
            };
            await _reportViewModelService.PrepareBreakdownJobReportModel(output, "Vishnu", true);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> ReadBreakdownData(DataSourceRequest command, BreakdownJobListModel model)
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
        public async Task<IActionResult> ReadData1(DataSourceRequest command, BreakdownJobReportListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (VesselName != null)
                {

                    var breakdownJobReports = await _reportService.GetAllBreakdownJobReports(model.SearchName, command.Page - 1, command.PageSize, true);
                    List<BreakdownJobReport> breakdownreports = new List<BreakdownJobReport>();
                    foreach (BreakdownJobReport item in breakdownJobReports)
                    {
                        breakdownreports.Add(item);
                    }
                    var gridModel = new DataSourceResult { Data = breakdownreports.ToList() };
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBreakdownJobDetails(BreakdownJobModel addNewBreakdownJob)

        {
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs("", 0, 500, true);
            List<BreakdownJob> breakdownlist = new List<BreakdownJob>();
            int max = 9999;
            foreach (BreakdownJob item in breakdownJobs)
            {
                if (item.JobOrder > max)
                {
                    max = item.JobOrder;
                }
            }
            addNewBreakdownJob.JobOrder = max + 1;
            await _breakdownJobViewModelService.PrepareBreakdownJobModel(addNewBreakdownJob, "Breakdown", true);
            return RedirectToAction("List", "BreakdownJob");
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

                var vessel = await _breakdownJobService.GetBreakdownJobById(item.Id);
                vessel.EquipmentName = item.EquipmentName;
                vessel.JobOrder = item.JobOrder;

                vessel.Title = item.Title;
                vessel.JobReportedDate = item.JobReportedDate;
                vessel.ReportedBy = item.ReportedBy;
                vessel.Status = item.Status;


                await _breakdownJobService.UpdateBreakdownJob(vessel);
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
