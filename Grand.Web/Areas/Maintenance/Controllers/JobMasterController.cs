using Grand.Services.JobMaster;
using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class JobMasterController:BaseAdminController
      {
       private readonly IJobMasterViewModelService _jobmasterViewModelService;
        private readonly IJobMasterService _JobMasterService;
        public JobMasterController(IJobMasterViewModelService _jobmasterViewModelService, IJobMasterService _JobMasterService)
        {
            this._jobmasterViewModelService = _jobmasterViewModelService;
            this._JobMasterService = _JobMasterService;
        }
        [HttpGet]
        public IActionResult Create()
         {
              return View();
         }
        [HttpGet]
        public IActionResult AddJobMaster(JobMasterModel addJobMaster)
           {
            _jobmasterViewModelService.InsertJobMasterModel(addJobMaster);
            return RedirectToAction("Create");
           }
    }
}

    
