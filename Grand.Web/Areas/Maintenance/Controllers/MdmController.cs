﻿using Grand.Core.Domain.MakerEntity;
using Grand.Framework.Kendoui;
using Grand.Services.EquipmentType;
using Grand.Services.JobType;
using Grand.Services.Maker;
using Grand.Services.ReportedBy;
using Grand.Services.Cbm;
using Grand.Web.Areas.Admin.Controllers;

using Grand.Web.Areas.Maintenance.DomainModels;

using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class MdmController : BaseAdminController
    {
        private readonly IJobTypeService _jobTypeService;
        private readonly IJobTypeViewModelService _jobTypeViewModelService;
        private readonly IReportedByService _reportedByService;
        private readonly IReportedByViewModelService1 _reportedByViewModelService;

        private readonly ICbmService _cbmService;
        private readonly ICbmViewModelService _cbmViewModelService;
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IEquipmentTypeViewModelService _equipmentTypeViewModelService;
        private readonly IMakerService _makerService;

        private readonly IMakerService1 _makerService1;
        private readonly IMakerViewModelService _makerViewModelService;
        private readonly IMakerViewModelService1 _makerViewModelService1;
        public MdmController(IReportedByViewModelService1 _reportedByViewModelService, IReportedByService _reportedByService, IJobTypeViewModelService _jobTypeViewModelService, IJobTypeService _jobTypeService, IEquipmentTypeViewModelService _equipmentTypeViewModelService, IEquipmentTypeService _equipmentTypeService,IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1)
        public MdmController(IJobTypeViewModelService _jobTypeViewModelService, IJobTypeService _jobTypeService, IEquipmentTypeViewModelService _equipmentTypeViewModelService, IEquipmentTypeService _equipmentTypeService,IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1,ICbmService _cbmService,ICbmViewModelService _cbmViewModelService)
        {
            this._makerViewModelService = _makerViewModelService;
            this._makerService = _makerService;
            this._makerViewModelService1 = _makerViewModelService1;
            this._makerService1 = _makerService1;
            this._equipmentTypeService = _equipmentTypeService;
            this._equipmentTypeViewModelService = _equipmentTypeViewModelService;
            this._jobTypeService = _jobTypeService;
            this._jobTypeViewModelService = _jobTypeViewModelService;
            this._reportedByService = _reportedByService;
            this._reportedByViewModelService = _reportedByViewModelService;
             }
            this._cbmService = _cbmService;
            this._cbmViewModelService = _cbmViewModelService;

        }

        // list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> AddMakerModel()
        {
            var model = new MakerListModel();
            var makers = await _makerService.GetAllMakers("", 0, 500, true);
            List<Maker> makerList = makers.ToList();
            var gridModel = new DataSourceResult 
            {
                Data = makers.ToList()
            };

            return View(makerList);
        }

       [HttpPost]
        public async Task<IActionResult> AddMakerModel(DataSourceRequest command, MakerListModel model)
        {
            var makers = await _makerService.GetAllMakers("", 0, 500, true);
            return View(makers);      
        }

        [HttpGet]
        public async Task<IActionResult> AddCBM()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddMaker()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddReportedBy()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEquipmentType()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddJobType()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> AddMakerModel()
        //{
        //    var model = await Task.FromResult<object>(null);
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> MdmList()
        {
            var model = await Task.FromResult<object>(null);
            var makers = await _makerService.GetAllMakers("", 0, 500, true);
            return View(makers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMakerDetails(MakerModel addNewMaker)
        {
            await _makerViewModelService.PrepareMakerModel(addNewMaker, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCBMDetails(CBMModel addNewCBM)
        {
            await _cbmViewModelService.PrepareCbmModel(addNewCBM, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobTypeDetails(JobTypeModel addNewJobType)
        {
            await _jobTypeViewModelService.PrepareJobTypeModel(addNewJobType, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEquipmentTypeDetails(EquipmentTypeModel addNewEquipmentType)
        {
            await _equipmentTypeViewModelService.PrepareEquipmentTypeModel(addNewEquipmentType, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReportedByDetails(ReportedByModel addNewReportedBy)
        {
            await _reportedByViewModelService.PrepareReportedByModel(addNewReportedBy, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpPost]
        public async Task<IActionResult> ReadEquipmentTypeDetails(DataSourceRequest command, EquipmentTypeModel model)
        {
            var equipmentTypelist = await _equipmentTypeService.GetAllEquipmentTypes("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = equipmentTypelist.ToList() };
            return Json(gridModel);

        }

        [HttpPost]
        public async Task<IActionResult> ReadReportedByDetails(DataSourceRequest command, ReportedByModel model)
        {
            var reportedBylist = await _reportedByService.GetAllReportedBy("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = reportedBylist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadJobTypeDetails(DataSourceRequest command, JobTypeModel model)
        {
            var jobTypelist = await _jobTypeService.GetAllJobTypes("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = jobTypelist.ToList() };
            return Json(gridModel);

        }

        [HttpPost]
        public async Task<IActionResult> ReadCBMDetails(DataSourceRequest command, CBMModel model)
        {
            var cbmlist = await _cbmService.GetAllCbm("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = cbmlist.ToList() };
            return Json(gridModel);

        }

        [HttpGet]
        public async Task<IActionResult> AddModelDetails(MakerModel1 addNewMaker1)
        {
            await _makerViewModelService1.PrepareMakerModel(addNewMaker1, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpPost]
        public async Task<IActionResult> ReadMakerDetails(DataSourceRequest command, MakerModel model)
        {
            var makerlist = await _makerService.GetAllMakers("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = makerlist.ToList()};
            return Json(gridModel);  
           
        }

        [HttpPost]
        public async Task<IActionResult> ReadMakerModelDetails(DataSourceRequest command, MakerModel model)
        {
            var makerlist = await _makerService1.GetAllMakers("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = makerlist.ToList() };
            return Json(gridModel);

        }
    }
}
