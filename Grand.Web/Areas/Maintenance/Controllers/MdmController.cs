using Grand.Core.Domain.MakerEntity;
using Grand.Framework.Kendoui;
using Grand.Services.EquipmentType;
using Grand.Services.JobStatus;
using Grand.Services.JobType;
using Grand.Services.Maker;
using Grand.Services.ReportedBy;
using Grand.Services.Cbm;
using Grand.Services.CbmMapping;
using Grand.Web.Areas.Admin.Controllers;
using Grand.Services.Jobplan;
using Grand.Web.Areas.Maintenance.DomainModels;

using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Services.Department;
using Grand.Services.Location;
using Grand.Services.SafetyLevel;
using Grand.Services.EquipmentStatus;
using Grand.Services.Frequency;
using Grand.Services.FrequencyType;
using Grand.Services.Rank;
using Grand.Services.MaintenanceType;
using Grand.Services.Priority;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class MdmController : BaseAdminController
    {
        private readonly IJobTypeService _jobTypeService;
        private readonly IJobTypeViewModelService _jobTypeViewModelService;
        private readonly IJobStatusService _jobStatusService;
        private readonly IJobStatusViewModelService _jobStatusViewModelService;
        private readonly IEquipmentStatusService _equipmentStatusService;
        private readonly IEquipmentStatusViewModelService _equipmentStatusViewModelService;

        private readonly IReportedByService _reportedByService;
        private readonly IReportedByViewModelService1 _reportedByViewModelService;

        private readonly ICbmService _cbmService;
        private readonly ICbmViewModelService _cbmViewModelService;

        private readonly ICbmMappingService _cbmMappingService;
        private readonly ICbmMappingViewModelService _cbmMappingViewModelService;

        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IEquipmentTypeViewModelService _equipmentTypeViewModelService;
        private readonly IMakerService _makerService;
        private readonly IDepartmentService _departmentService;
        private readonly IDepartmentViewModelService _departmentViewModelService;
        private readonly ILocationService _locationService;
        private readonly ILocationViewModelService _locationViewModelService;
        private readonly ISafetyLevelService _safetyLevelService;
        private readonly ISafetyLevelViewModelService _safetyLevelViewModelService;

        private readonly IMakerService1 _makerService1;
        private readonly IMakerViewModelService _makerViewModelService;
        private readonly IMakerViewModelService1 _makerViewModelService1;
        private readonly IFrequencyService _frequencyService;
        private readonly IFrequencyViewModelService _frequencyViewModelService;
        private readonly IFrequencyTypeService _frequencyTypeService;
        private readonly IFrequencyTypeViewModelService _frequencyTypeViewModelService;
        private readonly IRankService _rankService;
        private readonly IRankViewModelService _rankViewModelService;
        private readonly IMaintenanceTypeService _maintenanceTypeService;
        private readonly IMaintenanceTypeViewModelService _maintenanceTypeViewModelService;
        private readonly IPriorityService _priorityService;
        private readonly IPriorityViewModelService _priorityViewModelService;

        private readonly IJobplanService _jobplanService;
        //public MdmController(IReportedByViewModelService1 _reportedByViewModelService, IReportedByService _reportedByService, IJobStatusViewModelService _jobStatusViewModelService, IJobTypeViewModelService _jobTypeViewModelService, IJobStatusService _jobStatusService, IJobTypeService _jobTypeService, IEquipmentTypeViewModelService _equipmentTypeViewModelService, IEquipmentTypeService _equipmentTypeService,IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1)
        //public MdmController(IReportedByViewModelService1 _reportedByViewModelService, IReportedByService _reportedByService, IJobTypeViewModelService _jobTypeViewModelService, IJobTypeService _jobTypeService, IEquipmentTypeViewModelService _equipmentTypeViewModelService, IEquipmentTypeService _equipmentTypeService,IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1)
        public MdmController(IPriorityViewModelService _priorityViewModelService,IPriorityService _priorityService,IMaintenanceTypeViewModelService _maintenanceTypeViewModelService,IMaintenanceTypeService _maintenanceTypeService,IRankViewModelService _rankViewModelService,IRankService _rankService,IFrequencyTypeViewModelService _frequencyTypeViewModelService,IFrequencyTypeService _frequencyTypeService,IFrequencyViewModelService _frequencyViewModelService,IFrequencyService _frequencyService,IEquipmentStatusViewModelService _equipmentStatusViewModelService, IEquipmentStatusService _equipmentStatusService, ISafetyLevelViewModelService _safetyLevelViewModelService, ISafetyLevelService _safetyLevelService, ILocationViewModelService _locationViewModelService, ILocationService _locationService, IDepartmentViewModelService _departmentViewModelService, IDepartmentService _departmentService, IJobplanService _jobplanService, ICbmMappingViewModelService cbmMappingViewModelService, ICbmMappingService cbmMappingService, IJobStatusViewModelService _jobStatusViewModelService, IJobStatusService _jobStatusService, IReportedByViewModelService1 _reportedByViewModelService, IReportedByService _reportedByService, IJobTypeViewModelService _jobTypeViewModelService, IJobTypeService _jobTypeService, IEquipmentTypeViewModelService _equipmentTypeViewModelService, IEquipmentTypeService _equipmentTypeService, IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1, ICbmService _cbmService, ICbmViewModelService _cbmViewModelService)
        {
            this._cbmMappingViewModelService = cbmMappingViewModelService;
            this._cbmMappingService = cbmMappingService;
            this._makerViewModelService = _makerViewModelService;
            this._makerService = _makerService;
            this._makerViewModelService1 = _makerViewModelService1;
            this._makerService1 = _makerService1;
            this._equipmentTypeService = _equipmentTypeService;
            this._equipmentTypeViewModelService = _equipmentTypeViewModelService;
            this._jobTypeService = _jobTypeService;
            this._jobTypeViewModelService = _jobTypeViewModelService;
            this._jobStatusService = _jobStatusService;
            this._jobStatusViewModelService = _jobStatusViewModelService;
            this._equipmentStatusService = _equipmentStatusService;
            this._equipmentStatusViewModelService = _equipmentStatusViewModelService;
            this._frequencyService = _frequencyService;
            this._frequencyViewModelService = _frequencyViewModelService;
            this._frequencyTypeService = _frequencyTypeService;
            this._frequencyTypeViewModelService = _frequencyTypeViewModelService;
            this._maintenanceTypeService = _maintenanceTypeService;
            this._maintenanceTypeViewModelService = _maintenanceTypeViewModelService;

            this._jobplanService = _jobplanService;
            this._reportedByService = _reportedByService;
            this._reportedByViewModelService = _reportedByViewModelService;
            this._departmentService = _departmentService;
            this._departmentViewModelService = _departmentViewModelService;
            this._locationService = _locationService;
            this._locationViewModelService = _locationViewModelService;
            this._safetyLevelService = _safetyLevelService;
            this._safetyLevelViewModelService = _safetyLevelViewModelService;
            this._rankService = _rankService;
            this._rankViewModelService = _rankViewModelService;
            this._priorityService = _priorityService;
            this._priorityViewModelService = _priorityViewModelService;

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
            var gridModel = new DataSourceResult {
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



        public async Task<IActionResult> NewCBMMapping()
        {
            ViewModel vm = new ViewModel();

            List<string> eqpTypeList = new List<string>();
            var equipmentType = await _equipmentTypeService.GetAllEquipmentTypes("", 0, 500, true);
            var equipmentTypeList = equipmentType.ToList();

            var cbm = await _cbmService.GetAllCbm("", 0, 500, true);
            var cbmList = cbm.ToList();

            var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
            var jobplansList = jobplans.ToList();


            foreach (var item in equipmentTypeList)
            {
                eqpTypeList.Add(item.Equipment_type);
            }
            // vm.equipmentTypeList = equipmentTypeList;
            vm.EquipmentTypeList = eqpTypeList;


            vm.cbmList = cbmList;
            vm.JobplansList = jobplansList;

            return View(vm);
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
        public async Task<IActionResult> AddDepartment()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddFrequency()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddPriority()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddMaintenanceType()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddRank()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddFrequencyType()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddLocation()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddSafetyLevel()
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
        [HttpGet]
        public async Task<IActionResult> AddJobStatus()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEquipmentStatus()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }

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
        public async Task<IActionResult> AddRankDetails(RankModel addNewRank)
        {
            await _rankViewModelService.PrepareRankModel(addNewRank, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddCBMDetail(string CBM_Name)
        {
            CBMModel addNewCBM = new CBMModel() { CBM_Name=CBM_Name};
            await _cbmViewModelService.PrepareCbmModel(addNewCBM, "", true);
            return Json("");
        }

        [HttpGet]
        public async Task<IActionResult> AddCBMMapping(CBMMappingModel cbmMappingModel, string selcetedCBM)
        {
            await _cbmMappingViewModelService.PrepareCbmMappingModel(cbmMappingModel, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobTypeDetails(JobTypeModel addNewJobType)
        {
            await _jobTypeViewModelService.PrepareJobTypeModel(addNewJobType, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddJobTypeDetail(string Job_type)
        {
            JobTypeModel addNewJobType = new JobTypeModel() { Job_type = Job_type };
            await _jobTypeViewModelService.PrepareJobTypeModel(addNewJobType, "", true);
            return Json("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobStatusDetails(JobStatusModel addNewJobStatus)
        {
            await _jobStatusViewModelService.PrepareJobStatusModel(addNewJobStatus, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddJobStatusDetail(string Status)
        {
            JobStatusModel addNewJobStatus = new JobStatusModel();
            addNewJobStatus.Status = Status;
            await _jobStatusViewModelService.PrepareJobStatusModel(addNewJobStatus, "", true);
            return Json("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEquipmentStatusDetails(EquipmentStatusModel addNewEquipmentStatus)
        {
            await _equipmentStatusViewModelService.PrepareEquipmentStatusModel(addNewEquipmentStatus, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddEquipmentStatusDetail(string Status)
        {
            EquipmentStatusModel addNewEquipmentStatus = new EquipmentStatusModel { Status = Status };
            await _equipmentStatusViewModelService.PrepareEquipmentStatusModel(addNewEquipmentStatus, "", true);
            return Json("");
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
        public async Task<IActionResult> AddSafetyLevelDetails(SafetyLevelModel addNewSafetyLevel)
        {
            await _safetyLevelViewModelService.PrepareSafetyLevelModel(addNewSafetyLevel, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddSafetyLevelDetail(string Safety_level)
        {
            SafetyLevelModel addNewSafetyLevel = new SafetyLevelModel();
            addNewSafetyLevel.Safety_level = Safety_level;
            await _safetyLevelViewModelService.PrepareSafetyLevelModel(addNewSafetyLevel, "", true);
            return Json("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLocationDetails(LocationModel addNewLocation)
        {
            await _locationViewModelService.PrepareLocationModel(addNewLocation, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }


        [HttpGet]
        public async Task<IActionResult> AddLocationDetail(string Locations)
        {
            LocationModel addNewLocation = new LocationModel();
            addNewLocation.Locations = Locations;
            await _locationViewModelService.PrepareLocationModel(addNewLocation, "", true);
            return Json("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPriorityDetails(PriorityModel addNewPriority)
        {
            await _priorityViewModelService.PreparePriorityModel(addNewPriority, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMaintenanceTypeDetails(MaintenanceTypeModel addNewMaintenanceType)
        {
            await _maintenanceTypeViewModelService.PrepareMaintenanceTypeModel(addNewMaintenanceType, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFrequencyDetails(FrequencyModel addNewFrequency)
        {
            await _frequencyViewModelService.PrepareFrequencyModel(addNewFrequency, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddFrequencyDetail(string Frequencie)
        {
            FrequencyModel addNewFrequency = new FrequencyModel();
            addNewFrequency.Frequencies = Frequencie;
            await _frequencyViewModelService.PrepareFrequencyModel(addNewFrequency, "", true);
            return Json("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFrequencyTypeDetails(FrequencyTypeModel addNewFrequencyType)
        {
            await _frequencyTypeViewModelService.PrepareFrequencyTypeModel(addNewFrequencyType, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartmentDetails(DepartmentModel addNewDepartment)
        {
            await _departmentViewModelService.PrepareDepartmentModel(addNewDepartment, "", true);
            return RedirectToAction("MdmList", "Mdm");
           
        }

        [HttpGet]
        public async Task<IActionResult> AddDepartments(string Departments)
        {
            DepartmentModel addNewDepartment = new DepartmentModel();
            addNewDepartment.Departments = Departments;
            await _departmentViewModelService.PrepareDepartmentModel(addNewDepartment, "", true);
            // return RedirectToAction("MdmList", "Mdm");
            return Json("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReportedByDetails(ReportedByModel addNewReportedBy)
        {
            await _reportedByViewModelService.PrepareReportedByModel(addNewReportedBy, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddReportedByDetails(string Reported_By)
        {
            ReportedByModel addNewReportedBy = new ReportedByModel();
            addNewReportedBy.Reported_By = Reported_By;
            await _reportedByViewModelService.PrepareReportedByModel(addNewReportedBy, "", true);
            return Json("");
        }



        [HttpPost]
        public async Task<IActionResult> ReadCBMMappingDetails(DataSourceRequest command, ReportedByModel model)
        {
            var cbmMappinglist = await _cbmMappingService.GetAllCbmMapping("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = cbmMappinglist.ToList() };
            return Json(gridModel);

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
        public async Task<IActionResult> ReadDepartmentDetails(DataSourceRequest command, DepartmentModel model)
        {
            var departmentlist = await _departmentService.GetAllDepartments("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = departmentlist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadFrequencyDetails(DataSourceRequest command, FrequencyModel model)
        {
            var frequencylist = await _frequencyService.GetAllFrequencies("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = frequencylist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadRankDetails(DataSourceRequest command, RankModel model)
        {
            var ranklist = await _rankService.GetAllRanks("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = ranklist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadFrequencyTypeDetails(DataSourceRequest command, FrequencyTypeModel model)
        {
            var frequencyTypelist = await _frequencyTypeService.GetAllFrequencyTypes("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = frequencyTypelist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadPriorityDetails(DataSourceRequest command, PriorityModel model)
        {
            var prioritylist = await _priorityService.GetAllPriorities("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = prioritylist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadLocationDetails(DataSourceRequest command, LocationModel model)
        {
            var locationlist = await _locationService.GetAllLocations("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = locationlist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadMaintenanceTypeDetails(DataSourceRequest command, MaintenanceTypeModel model)
        {
            var maintenanceTypelist = await _maintenanceTypeService.GetAllMaintenanceTypes("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = maintenanceTypelist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadSafetyLevelDetails(DataSourceRequest command,SafetyLevelModel model)
        {
            var safetylevellist = await _safetyLevelService.GetAllSafetyLevels("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = safetylevellist.ToList() };
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
        public async Task<IActionResult> ReadJobStatusDetails(DataSourceRequest command, JobStatusModel model)
        {
            var jobStatuslist = await _jobStatusService.GetAllJobStatus("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = jobStatuslist.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadEquipmentStatusDetails(DataSourceRequest command, EquipmentStatusModel model)
        {
            var equipmentStatuslist = await _equipmentStatusService.GetAllEquipmentStatus("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = equipmentStatuslist.ToList() };
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

        [HttpGet]
        public async Task<IActionResult> AddModelDetail(string Maker,string Model, string Remark, string SearchName)
        {
            MakerModel1 addNewMaker1 = new MakerModel1() {
                Maker = Maker,
                Model = Model,
                Remark = Remark,
                SearchName = SearchName
            };
            await _makerViewModelService1.PrepareMakerModel(addNewMaker1, "", true);
            return Json("");
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
