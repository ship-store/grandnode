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

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class UnplannedJobsController : BaseAdminController
    {
        private readonly IUnplannedJobService _unplannedJobService;

        private readonly IUnplannedJobViewModelService _unplannedJobViewModelService;
        public UnplannedJobsController(IUnplannedJobViewModelService _unplannedJobViewModelService, IUnplannedJobService _unplannedJobService)
        {

            this._unplannedJobViewModelService = _unplannedJobViewModelService;
            this._unplannedJobService = _unplannedJobService;

        }

        // list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> List()
        {

            //var model = new UnplannedJobListModel();
             // await Task.FromResult(0);          

            //var unplannedJobs = await _unplannedJobService.GetAllUnplannedJobs("Vishnu", 0, 500, true);


            //List<UnplannedJob> unplannedJobList = unplannedJobs.ToList();
            //var gridModel = new DataSourceResult {
            //    Data = unplannedJobs.ToList()

            //};

            return View();
            // return View();

        }


        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command, UnplannedJobListModel model)
        {
            var unplannedJobs = await _unplannedJobService.GetAllUnplannedJobs(model.SearchName, command.Page - 1, command.PageSize, true);

            var gridModel = new DataSourceResult {
                Data = unplannedJobs.ToList()

            };
            return Json(gridModel);
        }


        [HttpGet]
        public async Task<IActionResult> AddUnplannedJobs()
        {
            var model = await Task.FromResult<object>(null);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddUnplannedJobDetails(UnplannedJobModel addNewUnplannedJob)
        {
            await _unplannedJobViewModelService.PrepareUnplannedJobModel(addNewUnplannedJob, "Vishnu", true);
            return RedirectToAction("List", "UnplannedJobs");
        }
        public async Task<IActionResult> Edit(string id)
        {
            var unplannedjob = await _unplannedJobService.GetUnplannedJobById(id);
            UnplannedJobDisplayModel display = new UnplannedJobDisplayModel { UnplannedJobID = unplannedjob.Id, EquipmentName = unplannedjob.EquipmentName, JobOrder = unplannedjob.JobOrder, Title = unplannedjob.Title, JobReportedDate = unplannedjob.JobReportedDate, ReportedBy = unplannedjob.ReportedBy, Status = unplannedjob.Status };

            if (unplannedjob == null)
                //No product found with the specified id
                return RedirectToAction("List");

            return View(display);
        }


        [HttpGet]
        public async Task<IActionResult> EditItem(UnplannedJobDisplayModel unplannedjobForDisplay, string id)
        {
            id = unplannedjobForDisplay.UnplannedJobID;
            var unplannedJob = await _unplannedJobService.GetUnplannedJobById(id);
            unplannedJob.EquipmentName = unplannedjobForDisplay.EquipmentName;
            unplannedJob.JobOrder = unplannedjobForDisplay.JobOrder;

            unplannedJob.Title = unplannedjobForDisplay.Title;
            unplannedJob.JobReportedDate = unplannedjobForDisplay.JobReportedDate;
            unplannedJob.ReportedBy = unplannedjobForDisplay.ReportedBy;
            unplannedJob.Status = unplannedjobForDisplay.Status;


            await _unplannedJobService.UpdateUnplannedJob(unplannedJob);
            return RedirectToAction("List");
        }


    }
}
