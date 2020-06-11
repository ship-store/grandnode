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
using Grand.Core.Domain.Equipment;
using Grand.Services.Equipments;
using System;
using Grand.Services.Report;
using Grand.Core.Domain.Report;
using Grand.Services.ReportedBy;
using Grand.Services.JobStatus;
using Grand.Core.Domain.ReportedByEntity;
using Grand.Core.Domain.JobStatusEntity;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class UnplannedJobsController : BaseAdminController
    {
        private readonly IUnplannedJobService _unplannedJobService;
        private readonly IReportService _reportService;
        private readonly IEquipmentService _equipmentService;
        private readonly IReportedByService _reportedByService;
        private readonly IJobStatusService _jobStatusService;
        private readonly IUnplannedJobViewModelService _unplannedJobViewModelService;
        private readonly IReportViewModelService _reportViewModelService;
        public UnplannedJobsController(IEquipmentService _equipmentService, IUnplannedJobViewModelService _unplannedJobViewModelService, IUnplannedJobService _unplannedJobService, IReportService _reportService, IReportViewModelService _reportViewModelService, IReportedByService _reportedByService, IJobStatusService _jobStatusService)
        {
            this._unplannedJobViewModelService = _unplannedJobViewModelService;
            this._unplannedJobService = _unplannedJobService;
            this._equipmentService = _equipmentService;
            this._reportService = _reportService;
            this._reportViewModelService = _reportViewModelService;
            this._reportedByService = _reportedByService;
            this._jobStatusService = _jobStatusService;
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
            var vessel = await _unplannedJobService.GetUnplannedJobById(id);

            vessel.JobCompletedDate = today.ToString("yyyy-MM-dd");
            vessel.Status = "Completed";
            vessel.Remark = remark;

            await _unplannedJobService.UpdateUnplannedJob(vessel);
            //ReportModel emp = vessel.Cast<ReportModel>();
            ReportModel output = new ReportModel() {
                EquipmentName = vessel.EquipmentName,
                JobOrder = vessel.JobOrder,
                Category = vessel.Category,
                Title = vessel.Title,
                JobReportedDate = vessel.JobReportedDate,
                ReportedBy = vessel.ReportedBy,
                Status = vessel.Status,
                Vessel = vessel.Vessel,
                DeleteStatus = vessel.DeleteStatus,
                JobCompletedDate = vessel.JobCompletedDate,
                Remark = remark

            };

            await _reportViewModelService.PrepareReportModel(output, "Vishnu", true);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> ReadData(DataSourceRequest command, UnplannedJobListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (VesselName != null)
                {

                    var unplannedJobs = await _unplannedJobService.GetAllUnplannedJobs(model.SearchName, command.Page, command.PageSize, true);
                    List<UnplannedJob> unplannedlist = new List<UnplannedJob>();
                    foreach (UnplannedJob item in unplannedJobs.Where(x => x.Vessel == VesselName))
                    {
                        unplannedlist.Add(item);
                    }
                    var gridModel = new DataSourceResult { Data = unplannedlist.ToList().Where(x => x.DeleteStatus != "1") };
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

        public async Task<IActionResult> ReadData1(DataSourceRequest command, UnplannedJobReportListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (VesselName != null)
                {

                    var unplannedJobReports = await _reportService.GetAllUnplannedJobReports(model.SearchName, command.Page, command.PageSize, true);
                    List<Report> unplannedreports = new List<Report>();
                    foreach (Report item in unplannedJobReports.Where(x => x.Vessel == VesselName))
                    {
                        unplannedreports.Add(item);
                    }
                    var gridModel = new DataSourceResult { Data = unplannedreports.ToList() };
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


        [HttpGet]
        public async Task<IActionResult> AddUnplannedJobs()
        {
            var reporters = await _reportedByService.GetAllReportedBy("", 0, 500, true);
            List<ReportedBy> reporterList = reporters.ToList();
            ViewBag.ReporterList = reporterList;

            var statuses = await _jobStatusService.GetAllJobStatus("", 0, 500, true);
            List<JobStatus> statusList = statuses.ToList();
            ViewBag.StatusList = statusList;

            var model = await Task.FromResult<object>(null);

            var VesselName = HttpContext.Session.GetString("VesselName").ToString().ToLower();
            var unplannedJobs = await _unplannedJobService.GetAllUnplannedJobs("", 0, 500, true);
            List<UnplannedJob> unplannedlist = new List<UnplannedJob>();
            int max = 9999;
            foreach (UnplannedJob item in unplannedJobs)
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
        public async Task<IActionResult> AddUnplannedJobDetails(UnplannedJobModel addNewUnplannedJob)
        {

            var unplannedJobs = await _unplannedJobService.GetAllUnplannedJobs("", 0, 500, true);
            List<UnplannedJob> unplannedlist = new List<UnplannedJob>();
            int max = 9999;
            string VesselName = HttpContext.Session.GetString("VesselName").ToString();
            foreach (UnplannedJob item in unplannedJobs)
            {


                if (item.JobOrder > max)
                {
                    max = item.JobOrder;
                }
            }
            addNewUnplannedJob.JobOrder = max + 1;
            await _unplannedJobViewModelService.PrepareUnplannedJobModel(addNewUnplannedJob, "Vishnu", true);
            return RedirectToAction("List", "UnplannedJobs");
        }
        public async Task<IActionResult> Edit(string id)
        {
            var unplannedjob = await _unplannedJobService.GetUnplannedJobById(id);
            UnplannedJobDisplayModel display = new UnplannedJobDisplayModel { UnplannedJobID = unplannedjob.Id, EquipmentName = unplannedjob.EquipmentName, JobOrder = unplannedjob.JobOrder, Title = unplannedjob.Title, JobReportedDate = unplannedjob.JobReportedDate, ReportedBy = unplannedjob.ReportedBy, Status = unplannedjob.Status };
            if (unplannedjob == null)
                return RedirectToAction("List");
            return View(display);
        }

        [HttpPost]
        public async Task<JsonResult> EditItem(DataSourceRequest command, List<UnplannedJob> models)
        {
            foreach (var item in models)
            {
                var unplannedJob = await _unplannedJobService.GetUnplannedJobById(item.Id);
                unplannedJob.EquipmentName = item.EquipmentName;
                unplannedJob.JobOrder = item.JobOrder;
                unplannedJob.Title = item.Title;
                unplannedJob.JobReportedDate = item.JobReportedDate;
                unplannedJob.ReportedBy = item.ReportedBy;
                unplannedJob.Status = item.Status;

                await _unplannedJobService.UpdateUnplannedJob(unplannedJob);
            }
            return Json(models);
        }

        //[HttpGet]
        //public async Task<IActionResult> DeleteSelected(string selectedIds)
        //{
        //    string[] strlist = selectedIds.Split(",");
        //    var SelectedList = strlist.ToList();
        //    if (selectedIds != null)
        //    {
        //        for (int i = 0; i < strlist.Length; i++)
        //        {
        //            var breakdownjob = await _unplannedJobService.GetUnplannedJobById(strlist[i].Trim(new char[] { (char)39 }));
        //            await _unplannedJobViewModelService.DeleteUnplannedJob(breakdownjob);
        //        }
        //    }
        //    return Json(new { Result = true });
        //}

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


                    var selectedunplanedJob = await _unplannedJobService.GetUnplannedJobById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedunplanedJob.DeleteStatus = "1";//changin job to postponed
                    await _unplannedJobService.UpdateUnplannedJob(selectedunplanedJob);
                }
            }

            return Json(new { Result = true });
        }

    }
}
