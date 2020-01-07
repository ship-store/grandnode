using Grand.Core.Domain.BreakdownJob;
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
namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class BreakdownJobController : BaseAdminController
    {
        private readonly IBreakdownJobService _breakdownJobService;

        private readonly IBreakdownJobViewModelService _breakdownJobViewModelService;
        public BreakdownJobController(IBreakdownJobViewModelService _breakdownJobViewModelService, IBreakdownJobService _breakdownJobService)
        {

            this._breakdownJobViewModelService = _breakdownJobViewModelService;
            this._breakdownJobService = _breakdownJobService;

        }

        // list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> List()
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command, BreakdownJobListModel model)
        {
            var breakdownJobs = await _breakdownJobService.GetAllBreakdownJobs(model.SearchName, command.Page - 1, command.PageSize, true);

            var gridModel = new DataSourceResult {
                Data = breakdownJobs.ToList()

            };
            return Json(gridModel);
        }


        [HttpGet]
        public async Task<IActionResult> AddBreakdownJob()
        {
            var model = await Task.FromResult<object>(null);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddBreakdownJobDetails(BreakdownJobModel addNewBreakdownJob)
        {
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


        [HttpGet]
        public async Task<IActionResult> EditItem(BreakdownJobDisplayModel breakdownjobForDisplay, string id)
        {
            id = breakdownjobForDisplay.BreakdownJobID;
            var vessel = await _breakdownJobService.GetBreakdownJobById(id);
            vessel.EquipmentName = breakdownjobForDisplay.EquipmentName;
            vessel.JobOrder = breakdownjobForDisplay.JobOrder;

            vessel.Title = breakdownjobForDisplay.Title;
            vessel.JobReportedDate = breakdownjobForDisplay.JobReportedDate;
            vessel.ReportedBy = breakdownjobForDisplay.ReportedBy;
            vessel.Status = breakdownjobForDisplay.Status;
            

            await _breakdownJobService.UpdateBreakdownJob(vessel);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelected(string selectedIds)
        {
            string[] strlist = selectedIds.Split(",");
            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {
                    var breakdownjob = await _breakdownJobService.GetBreakdownJobById(strlist[i].Trim(new char[] { (char)39 }));
                    await _breakdownJobViewModelService.DeleteBreakdownJob(breakdownjob);
                }

            }

            return Json(new { Result = true });
        }


    }
}
