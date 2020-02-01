using Grand.Core.Domain.Jobplan;
using Grand.Framework.Kendoui;
using Grand.Services.Jobplan;
using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Maintenance.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class PostponeJobController : BaseAdminController
    {
        private readonly IJobplanService _jobplanService;
        public PostponeJobController(IJobplanService _jobplanService)
        {
            this._jobplanService = _jobplanService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //HttpContext.Session.SetString("Test", "Silpa");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command, JobplanListModel model)
        {
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            var Jobplanlist = await _jobplanService.GetAllJobplans(model.SearchName, command.Page - 1, 100, true);
            List<Jobplan> jobplanlist = new List<Jobplan>();
            foreach (Jobplan item in Jobplanlist.Where(x => x.Vessel == VesselName.ToLower()))
            {
                jobplanlist.Add(item);
            }
            var gridModel = new DataSourceResult { Data = jobplanlist.ToList().Where(x => x.JobStatus == 2) };
            return Json(gridModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReadData(DataSourceRequest command, JobplanListModel model)
        {
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            var Jobplanlist = await _jobplanService.GetAllJobplans(model.SearchName, command.Page - 1, 500, true);
            List<Jobplan> jobplanlist = new List<Jobplan>();

            foreach (Jobplan item in Jobplanlist.Where(x => x.Vessel == VesselName.ToLower() && x.NEXT_DUE_DATE != null))
            {
                var date = Convert.ToDateTime(item.NEXT_DUE_DATE);
                var dt = date.AddDays(-10).ToShortDateString();
                var today = DateTime.Now.ToShortDateString();

                if (today == dt)
                {
                    jobplanlist.Add(item);

                }
                //  jobplanlist.Add(item);
            }
            var gridModel = new DataSourceResult { Data = jobplanlist.ToList().Where(x => x.JobStatus == 2) };
            return Json(gridModel);
        }

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
                    days = Convert.ToInt32(item.Frequency) * 30;
                }
                else if (item.FrequencyType.ToLower() == "week")
                {
                    days = Convert.ToInt32(item.Frequency) * 7;
                }

                item.LAST_DONE_DATE = lastdone;
                var lastdonedate = Convert.ToDateTime(lastdone);
                item.NEXT_DUE_DATE = lastdonedate.AddDays(days).ToString("yyyy-MM-dd");
                item.JobStatus = 0;
                item.JobPlanStatus = "pending";


                await _jobplanService.UpdateJobPlan(item);
            }

            return RedirectToAction("List");

        }

        //[HttpGet]
        //public async Task<IActionResult> ExecuteItems(string selectedIds)
        //{
        //    await Task.FromResult(0);

        //    string[] strlist = selectedIds.Split(",");

        //    var SelectedList = strlist.ToList();
        //    if (selectedIds != null)
        //    {
        //        for (int i = 0; i < strlist.Length; i++)
        //        {


        //            var selectedJobplan = await _jobplanService.GetJobPlanById(strlist[i].Trim(new char[] { (char)39 }));

        //            selectedJobplan.JobStatus = 0;//changin job to postponed
        //            await _jobplanService.UpdateJobPlan(selectedJobplan);
        //        }
        //    }

        //    return Json(new { Result = true });
        //}


    }
}
