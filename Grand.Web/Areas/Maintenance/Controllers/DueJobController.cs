using Grand.Services.Jobplan;
using Grand.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Framework.Kendoui;
using Grand.Web.Areas.Maintenance.DomainModels;
using Microsoft.AspNetCore.Http;
using Grand.Core.Domain.Jobplan;
using Grand.Services.Report;
using Grand.Core.Domain.DueJobReport;
using Grand.Web.Areas.Maintenance.Interfaces;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    
    public class DueJobController : BaseAdminController
    {
       
        private readonly IJobplanService _jobplanService;
        private readonly IReportService _reportService;
        private readonly IReportViewModelService _reportViewModelService;
        public DueJobController(IReportViewModelService _reportViewModelService,IReportService _reportService,IJobplanService _jobplanService)
        {
            this._jobplanService = _jobplanService;
            this._reportService = _reportService;
            this._reportViewModelService = _reportViewModelService;
        }

        public IActionResult Index() => RedirectToAction("List");

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReadData(DataSourceRequest command, JobplanListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                var Jobplanlist = await _jobplanService.GetAllJobplans(model.SearchName, command.Page - 1, 500, true);
                List<Jobplan> jobplanlist = new List<Jobplan>();

                foreach (Jobplan item in Jobplanlist.Where(x => x.Vessel.ToLower() == VesselName.ToLower() && x.NEXT_DUE_DATE != null))
                {
                    var date = Convert.ToDateTime(item.NEXT_DUE_DATE);
                   /* var dt = date.AddDays(-10).ToShortDateString();
                    var today = DateTime.Now.ToShortDateString();
*/

                    var UperLimit = date.AddDays(10);
                    var temp = DateTime.Now.AddDays(10);

                

                    if (temp>=date)
                    {
                        jobplanlist.Add(item);
                    }

                    //if (today == dt)
                    //{
                    //    jobplanlist.Add(item);

                    //}
                    //  jobplanlist.Add(item);
                }
                var gridModel = new DataSourceResult { Data = jobplanlist.ToList().Where(x => x.JobStatus == 0) };
                return Json(gridModel);
            }
            catch (System.Exception)
            {

                return RedirectToAction("Success", "Register");
            }
            
        }



        [HttpPost]
        public async Task<IActionResult> ReadData2(DataSourceRequest command, JobplanListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                var Jobplanlist = await _jobplanService.GetAllJobplans(model.SearchName, command.Page - 1, 500, true);
                List<Jobplan> jobplanlist = new List<Jobplan>();

                foreach (Jobplan item in Jobplanlist.Where(x => x.Vessel.ToLower() == VesselName.ToLower() && x.NEXT_DUE_DATE != null).OrderBy(x => x.NEXT_DUE_DATE).ToList())
                {
                    var date = Convert.ToDateTime(item.NEXT_DUE_DATE);



                    var UperLimit = date.AddDays(10);
                    var temp = DateTime.Now.AddDays(10);
                    var dueRhs = (item.DueRhs / 100) * 10;
                    var duerhs1 = item.DueRhs - dueRhs;
                    var cureentrhs = item.LastReading;


                    if (temp >= date)
                    {
                        jobplanlist.Add(item);
                    }

                    if (cureentrhs <= duerhs1)
                    {
                        jobplanlist.Add(item);
                    }


                }
                var p = jobplanlist.OrderBy(x => x.NEXT_DUE_DATE).ToList();
                var gridModel = new DataSourceResult { Data = jobplanlist.ToList().Where(x => x.JobStatus == 0) };
                return Json(gridModel);
            }
            catch (System.Exception)
            {

                return RedirectToAction("Success", "Register");
            }

        }
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostponeJob(Jobplan model, string dt, string Bid)
        {
         var postponeJob = await _jobplanService.GetJobPlanById(Bid);
            var dueDate = Convert.ToDateTime(dt);
            var nextDueDate = dueDate.ToString("yyyy-MM-dd");
            postponeJob.NEXT_DUE_DATE = nextDueDate;
            postponeJob.JobStatus = 2;
            postponeJob.JobPlanStatus ="Postpone";
            await _jobplanService.UpdateJobPlan(postponeJob);
            return RedirectToAction("List");

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CompleteJob(Jobplan model, string dt, string Bid1, string remark,string vessel)
        //{
        //    var id = Bid1;
        //    var job = await _jobplanService.GetAllJobplan("", 0, 500, true);
        //    var jobplan = job.ToList().FindAll(y => y.Id == id);


        //    var postponeJob = await _jobplanService.GetJobPlanById(Bid1);
        //    var dueDate = Convert.ToDateTime(dt);
        //    var lastdone = dueDate.ToString("yyyy-MM-dd");

        //    var selectedJobPlan = jobplan;
        //    int days = 0;

        //    foreach (var item in jobplan)
        //    {
        //        if (item.FrequencyType.ToLower() == "month")
        //        {
        //            days = Convert.ToInt32(item.CalFrequency) * 30;
        //        }
        //        else if (item.FrequencyType.ToLower() == "week")
        //        {
        //            days = Convert.ToInt32(item.CalFrequency) * 7;
        //        }

        //        item.LAST_DONE_DATE = lastdone;
        //        var lastdonedate = Convert.ToDateTime(lastdone);
        //        item.NEXT_DUE_DATE = lastdonedate.AddDays(days).ToString("yyyy-MM-dd");
        //        item.JobStatus = 0;
        //        item.JobPlanStatus = "pending";
        //        item.Status= "Completed";


                
        //        DueJobReportModel output = new DueJobReportModel() {
        //            EquipmentCode = item.EquipmentCode,
        //            EquipmentName = item.EquipmentName,

        //            JobTitle = item.JobTitle,
        //            JobDescription = item.JobDescription,
        //            CalFrequency = item.CalFrequency,
        //            FrequencyType = item.FrequencyType,
        //            Vessel = item.Vessel,

        //            Department = item.Department,
        //            AssignedTo = item.AssignedTo,
        //            NEXT_DUE_DATE = item.NEXT_DUE_DATE,
        //            LAST_DONE_DATE = item.LAST_DONE_DATE,
        //            Job_Type = item.Job_Type,
        //            Maintenance_Type = item.Maintenance_Type,
        //            JobCompletedDate = item.JobCompletedDate,
        //            JobStatus = item.JobStatus,
        //            JobOrder = item.JobOrder,
        //            Reading = item.Reading,
        //            Remark = remark,
                   
        //        };

        //        await _jobplanService.UpdateJobPlan(item);
        //        await _reportViewModelService.PrepareDueJobReportModel(output, "Vishnu", true);
        //    }

        //    return RedirectToAction("List");

        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteJob(Jobplan model, string dt, string Bid1)
        {
            var id = Bid1;
            var job = await _jobplanService.GetAllJobplan("", 0, 500, true);
            var jobplan = job.ToList().FindAll(y => y.Id == id);

            var postponeJob = await _jobplanService.GetJobPlanById(Bid1);
            var dueDate = Convert.ToDateTime(dt);
            var lastdone = dueDate.ToString("yyyy-MM-dd");

            var selectedJobPlan = jobplan;
            int days = 0;

            foreach (var item in jobplan)
            {
                if (item.FrequencyType.ToLower() == "month")
                {
                    days = Convert.ToInt32(item.CalFrequency) * 30;
                }
                else if (item.FrequencyType.ToLower() == "week")
                {
                    days = Convert.ToInt32(item.CalFrequency) * 7;
                }

                item.LAST_DONE_DATE = lastdone;
                var lastdonedate = Convert.ToDateTime(lastdone);
                item.NEXT_DUE_DATE = lastdonedate.AddDays(days).ToString("yyyy-MM-dd");
                item.JobStatus = 0;
                item.JobPlanStatus = "pending";
                item.Status = "Completed";

                await _jobplanService.UpdateJobPlan(item);
            }

            return RedirectToAction("List");

        }
        public ActionResult List1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReadData1(DataSourceRequest command, DueJobReportListModel model)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (VesselName != null)
                {

                    var dueJobReports = await _reportService.GetAllDueJobReports(model.SearchName, command.Page - 1, command.PageSize, true);
                    List<DueJobReport> duereports = new List<DueJobReport>();
                    foreach (DueJobReport item in dueJobReports)
                    {
                        duereports.Add(item);
                    }
                    var gridModel = new DataSourceResult { Data = duereports.ToList() };
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

    }
}
