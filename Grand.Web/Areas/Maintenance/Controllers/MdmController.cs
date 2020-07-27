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
using Grand.Services.Critical;

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
        private readonly ICriticalService _criticalService;
        private readonly ICriticalViewModelService _criticalViewModelService;

        private readonly IJobplanService _jobplanService;
        public MdmController(ICriticalViewModelService _criticalViewModelService,ICriticalService _criticalService,IPriorityViewModelService _priorityViewModelService,IPriorityService _priorityService,IMaintenanceTypeViewModelService _maintenanceTypeViewModelService,IMaintenanceTypeService _maintenanceTypeService,IRankViewModelService _rankViewModelService,IRankService _rankService,IFrequencyTypeViewModelService _frequencyTypeViewModelService,IFrequencyTypeService _frequencyTypeService,IFrequencyViewModelService _frequencyViewModelService,IFrequencyService _frequencyService,IEquipmentStatusViewModelService _equipmentStatusViewModelService, IEquipmentStatusService _equipmentStatusService, ISafetyLevelViewModelService _safetyLevelViewModelService, ISafetyLevelService _safetyLevelService, ILocationViewModelService _locationViewModelService, ILocationService _locationService, IDepartmentViewModelService _departmentViewModelService, IDepartmentService _departmentService, IJobplanService _jobplanService, ICbmMappingViewModelService cbmMappingViewModelService, ICbmMappingService cbmMappingService, IJobStatusViewModelService _jobStatusViewModelService, IJobStatusService _jobStatusService, IReportedByViewModelService1 _reportedByViewModelService, IReportedByService _reportedByService, IJobTypeViewModelService _jobTypeViewModelService, IJobTypeService _jobTypeService, IEquipmentTypeViewModelService _equipmentTypeViewModelService, IEquipmentTypeService _equipmentTypeService, IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1, ICbmService _cbmService, ICbmViewModelService _cbmViewModelService)
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
            this._criticalService = _criticalService;
            this._criticalViewModelService = _criticalViewModelService;


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
            var jobplansList = jobplans.Where(x=>x.TakenOrNot!=1).ToList();

            var jobpanList2 = await _cbmMappingViewModelService.GetAll();
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
        public async Task<IActionResult> AddCritical()
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
        public async Task<IActionResult> AddMakerDetail(MakerModel addNewMaker)
        {
            await _makerViewModelService.PrepareMakerModel(addNewMaker, "", true);
            return Json("");
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
        public async Task<IActionResult> AddRankDetail(string Ranks)
        {
            RankModel addNewRank = new RankModel() { Ranks=Ranks};
            await _rankViewModelService.PrepareRankModel(addNewRank, "", true);
            return Json("");
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
            // list
           // await _cbmMappingViewModelService.GetAllCbmMappingAsList();
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

        [HttpGet]
        public async Task<IActionResult> AddEquipmentTypeDetail(string Equipment_type)
        {
            EquipmentTypeModel addNewEquipmentType = new EquipmentTypeModel() { Equipment_type=Equipment_type,};

            await _equipmentTypeViewModelService.PrepareEquipmentTypeModel(addNewEquipmentType, "", true);
            return Json("");
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


       [HttpGet]
        public async Task<IActionResult> AddPriorityDetail(string Priorities)
        {
            PriorityModel addNewPriority = new PriorityModel() { Priorities = Priorities };
            await _priorityViewModelService.PreparePriorityModel(addNewPriority, "", true);
            return Json("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMaintenanceTypeDetails(MaintenanceTypeModel addNewMaintenanceType)
        {
            await _maintenanceTypeViewModelService.PrepareMaintenanceTypeModel(addNewMaintenanceType, "", true);
            return RedirectToAction("MdmList", "Mdm");
        }

        [HttpGet]
        public async Task<IActionResult> AddMaintenanceTypeDetail(string Maintenance_type)
        {
            MaintenanceTypeModel addNewMaintenanceType = new MaintenanceTypeModel();
            addNewMaintenanceType.Maintenance_type = Maintenance_type;
            await _maintenanceTypeViewModelService.PrepareMaintenanceTypeModel(addNewMaintenanceType, "", true);
            return Json("");
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

        [HttpGet]
        public async Task<IActionResult> AddFrequencyTypeDetail(string Frequency_type)
        {
            FrequencyTypeModel addNewFrequencyType = new FrequencyTypeModel() {
                Frequency_type = Frequency_type
            };
            await _frequencyTypeViewModelService.PrepareFrequencyTypeModel(addNewFrequencyType, "", true);
            return Json("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartmentDetails(DepartmentModel addNewDepartment)
        {
            await _departmentViewModelService.PrepareDepartmentModel(addNewDepartment, "", true);
            return RedirectToAction("MdmList", "Mdm");
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCriticalDetails(CriticalModel addNewCritical)
        {
            await _criticalViewModelService.PrepareCriticalModel(addNewCritical, "", true);
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
        [HttpGet]
        public async Task<IActionResult> AddCriticals(string Criticals)
        {
            CriticalModel addNewCritical = new CriticalModel();
            addNewCritical.Criticals = Criticals;
            await _criticalViewModelService.PrepareCriticalModel(addNewCritical, "", true);
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
            var cbmMappingList = cbmMappinglist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = cbmMappingList.ToList() };
            return Json(gridModel);

        }

        [HttpPost]
        public async Task<IActionResult> ReadEquipmentTypeDetails(DataSourceRequest command, EquipmentTypeModel model)
        {
            var equipmentTypelist = await _equipmentTypeService.GetAllEquipmentTypes("", command.Page, command.PageSize);
            var equipmentTypeList1 = equipmentTypelist.Where(x=>x.DeleteStatus.ToString()==0.ToString()).ToList();
            //var resultList = equipmentTypeList1.Select(y => y.Equipment_type).Distinct().ToList();
            ////List<MakerModel> makerlist = new List<MakerModel>();
            //resultList.Reverse();
            //resultList.RemoveAt(0);
            //resultList.Reverse();
            var gridModel = new DataSourceResult { Data = equipmentTypeList1.GroupBy(x => x.Equipment_type).Select(x => x.First()).ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadDepartmentDetails(DataSourceRequest command, DepartmentModel model)
        {
            var departmentlist = await _departmentService.GetAllDepartments("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var departmentList= departmentlist.ToList().Where(x => x.DeleteStatus != 1);
            var gridModel = new DataSourceResult { Data = departmentList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadCriticalDetails(DataSourceRequest command, CriticalModel model)
        {
            var criticallist = await _criticalService.GetAllCriticals("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var criticalList = criticallist.ToList().Where(x => x.DeleteStatus != 1);
            var gridModel = new DataSourceResult { Data = criticalList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadFrequencyDetails(DataSourceRequest command, FrequencyModel model)
        {
            var frequencylist = await _frequencyService.GetAllFrequencies("", command.Page, command.PageSize);
            var frequencyList = frequencylist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = frequencyList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadRankDetails(DataSourceRequest command, RankModel model)
        {
            var ranklist = await _rankService.GetAllRanks("", command.Page, command.PageSize);
            var rankList = ranklist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = rankList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadFrequencyTypeDetails(DataSourceRequest command, FrequencyTypeModel model)
        {
            var frequencyTypelist = await _frequencyTypeService.GetAllFrequencyTypes("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var frequencyTypeList = frequencyTypelist.ToList().Where(x => x.DeleteStatus != 1);
            var gridModel = new DataSourceResult { Data = frequencyTypeList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadPriorityDetails(DataSourceRequest command, PriorityModel model)
        {
            var prioritylist = await _priorityService.GetAllPriorities("", command.Page, command.PageSize);
            var priorityList = prioritylist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = priorityList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadLocationDetails(DataSourceRequest command, LocationModel model)
        {
            var locationlist = await _locationService.GetAllLocations("", command.Page, command.PageSize);
            var locationList = locationlist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = locationList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadMaintenanceTypeDetails(DataSourceRequest command, MaintenanceTypeModel model)
        {
            var maintenanceTypelist = await _maintenanceTypeService.GetAllMaintenanceTypes("", command.Page, command.PageSize);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var maintenanceTypeList = maintenanceTypelist.ToList().Where(x => x.DeleteStatus != 1);
            var gridModel = new DataSourceResult { Data = maintenanceTypeList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadSafetyLevelDetails(DataSourceRequest command,SafetyLevelModel model)
        {
            var safetylevellist = await _safetyLevelService.GetAllSafetyLevels("", command.Page, command.PageSize);
            var safetylevelList = safetylevellist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = safetylevelList.ToList() };
            return Json(gridModel);

        }
        //[HttpPost]
        //public async Task<IActionResult> ReadReportedByDetails(DataSourceRequest command, ReportedByModel model)
        //{
        //    var reportedBylist = await _reportedByService.GetAllReportedBy("", command.Page, command.PageSize);
        //    //List<MakerModel> makerlist = new List<MakerModel>();
        //    var gridModel = new DataSourceResult { Data = reportedBylist.ToList() };
        //    return Json(gridModel);

        //}

        [HttpPost]
        public async Task<IActionResult> ReadReportedByDetails(DataSourceRequest command, ReportedByModel model)
        {
            var reportedBylist = await _reportedByService.GetAllReportedBy("", command.Page, command.PageSize);
            var reportedByList = reportedBylist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = reportedByList.ToList() };
            return Json(gridModel);

        }

        [HttpPost]
        public async Task<IActionResult> ReadJobTypeDetails(DataSourceRequest command, JobTypeModel model)
        {
            var jobTypelist = await _jobTypeService.GetAllJobTypes("", command.Page, command.PageSize);
            var jobTypeList = jobTypelist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = jobTypeList.ToList() };
            return Json(gridModel);
        }



        [HttpPost]
        public async Task<IActionResult> ReadJobStatusDetails(DataSourceRequest command, JobStatusModel model)
        {
            var jobStatuslist = await _jobStatusService.GetAllJobStatus("", command.Page, command.PageSize);
            var jobStatusList = jobStatuslist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = jobStatusList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadEquipmentStatusDetails(DataSourceRequest command, EquipmentStatusModel model)
        {
            var equipmentStatuslist = await _equipmentStatusService.GetAllEquipmentStatus("", command.Page, command.PageSize);
            var equipmentStatusList = equipmentStatuslist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = equipmentStatusList.ToList() };
            return Json(gridModel);

        }
        [HttpPost]
        public async Task<IActionResult> ReadCBMDetails(DataSourceRequest command, CBMModel model)
        {
            var cbmlist = await _cbmService.GetAllCbm("", command.Page, command.PageSize);
            var cbmList = cbmlist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = cbmList.ToList() };
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
            var makerList = makerlist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = makerList.ToList()};
            return Json(gridModel);  
           
        }

        [HttpPost]
        public async Task<IActionResult> ReadMakerModelDetails(DataSourceRequest command, MakerModel model)
        {
            var makerModellist = await _makerService1.GetAllMakers("", command.Page, command.PageSize);
            var makerModelList = makerModellist.ToList().Where(x => x.DeleteStatus != 1);
            //List<MakerModel> makerlist = new List<MakerModel>();
            var gridModel = new DataSourceResult { Data = makerModelList.ToList() };
           
            return Json(gridModel);

        }

        [HttpGet]
        public async Task<IActionResult> MakerList()
        {
            return PartialView("MakerList");
        }

        [HttpGet]
        public async Task<PartialViewResult> MakerModelList()
        {

            var model = await Task.FromResult<object>(null);
            var makers = await _makerService.GetAllMakers("", 0, 500, true);
            var makerList = makers.Where(x=>x.DeleteStatus!=1).ToList();

            ViewBag.makerList = makerList;
            return PartialView("MakerModelList",makers );
        }


        [HttpGet]
        public async Task<IActionResult> ReadReportedByDetails()
        {
            return PartialView("ReadReportedByDetails");
        }

        [HttpGet]
        public async Task<IActionResult> ReadJobStatusDetails()
        {
            return PartialView("ReadJobStatusDetails");
        }


        [HttpGet]
        public async Task<IActionResult> ReadEquipmentTypeDetails()
        {
            return PartialView("ReadEquipmentTypeDetails");
        }
        //
        [HttpGet]
        public async Task<IActionResult> ReadJobTypeDetails()
        {
            return PartialView("ReadJobTypeDetails");
        }

        //

        [HttpGet]
        public async Task<IActionResult> CBMList()
        {
            return PartialView("CBMList");
        }



        [HttpGet]
        public async Task<IActionResult> CBMMappingList()
        {
            return PartialView("CBMMappingList");
        }


        [HttpGet]
        public async Task<IActionResult> ReadDepartment()
        {
            return PartialView("ReadDepartment");
        }
        [HttpGet]
        public async Task<IActionResult> ReadCritical()
        {
            return PartialView("ReadCritical");
        }
        [HttpGet]
        public async Task<IActionResult> ReadLocation()
        {
            return PartialView("ReadLocation");
        }

        [HttpGet]
        public async Task<IActionResult> ReadSafetyLevel()
        {
            return PartialView("ReadSafetyLevel");
        }

        [HttpGet]
        public async Task<IActionResult> ReadEquipmentStatusDetails()
        {
            return PartialView("ReadEquipmentStatusDetails");
        }
        //

        [HttpGet]
        public async Task<IActionResult> ReadFrequencyDetails()
        {
            return PartialView("ReadFrequencyDetails");
        }


        [HttpGet]
        public async Task<IActionResult> ReadFrequencyTypeDetails()
        {
            return PartialView("ReadFrequencyTypeDetails");
        }


        [HttpGet]
        public async Task<IActionResult> ReadRankDetails()
        {
            return PartialView("ReadRankDetails");
        }

        //

        [HttpGet]
        public async Task<IActionResult> ReadMaintenanceTypeDetails()
        {
            return PartialView("ReadMaintenanceTypeDetails");
        }

        [HttpGet]
        public async Task<IActionResult> ReadPriorityDetails()
        {
            return PartialView("ReadPriorityDetails");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedReportedBy(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedReportedBy = await _reportedByService.GetReportedByById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedReportedBy.DeleteStatus = 1;//changin job to postponed
                    await _reportedByService.UpdateReportedBy(selectedReportedBy);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedJobStatus(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedJobStatus = await _jobStatusService.GetJobStatusById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedJobStatus.DeleteStatus = 1;//changin job to postponed
                    await _jobStatusService.UpdateJobStatus(selectedJobStatus);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
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


                    var selectedEquipmentComponent = await _equipmentTypeService.GetEquipmentTypeById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedEquipmentComponent.DeleteStatus = 1;//changin job to postponed
                    await _equipmentTypeService.UpdateEquipmentType(selectedEquipmentComponent);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedJobType(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedJobType = await _jobTypeService.GetJobTypeById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedJobType.DeleteStatus = 1;//changing job to postponed
                    await _jobTypeService.UpdateJobType(selectedJobType);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedCbm(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedCbm = await _cbmService.GetCbmById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedCbm.DeleteStatus = 1;//changing job to postponed
                    await _cbmService.UpdateCbm(selectedCbm);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedDepartment(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedDepartment = await _departmentService.GetDepartmentById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedDepartment.DeleteStatus = 1;//changing job to postponed
                    await _departmentService.UpdateDepartment(selectedDepartment);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedLocation(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedLocation = await _locationService.GetLocationById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedLocation.DeleteStatus = 1;//changing job to postponed
                    await _locationService.UpdateLocation(selectedLocation);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedSafetyLevel(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedSafetyLevel = await _safetyLevelService.GetSafetyLevelById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedSafetyLevel.DeleteStatus = 1;//changing job to postponed
                    await _safetyLevelService.UpdateSafetyLevel(selectedSafetyLevel);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedEquipmentStatus(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedEquipmentStatus = await _equipmentStatusService.GetEquipmentStatusById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedEquipmentStatus.DeleteStatus = 1;//changing job to postponed
                    await _equipmentStatusService.UpdateEquipmentStatus(selectedEquipmentStatus);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedFrequency(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedFrequency = await _frequencyService.GetFrequencyById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedFrequency.DeleteStatus = 1;//changing job to postponed
                    await _frequencyService.UpdateFrequency(selectedFrequency);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedFrequencyType(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedFrequencyType = await _frequencyTypeService.GetFrequencyTypeById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedFrequencyType.DeleteStatus = 1;//changing job to postponed
                    await _frequencyTypeService.UpdateFrequencyType(selectedFrequencyType);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedRank(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedRank = await _rankService.GetRankById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedRank.DeleteStatus = 1;//changing job to postponed
                    await _rankService.UpdateRank(selectedRank);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedMaintenanceType(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedMaintenanceType = await _maintenanceTypeService.GetMaintenanceTypeById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedMaintenanceType.DeleteStatus = 1;//changing job to postponed
                    await _maintenanceTypeService.UpdateMaintenanceType(selectedMaintenanceType);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("MdmList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSelectedPriority(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedPriority = await _priorityService.GetPriorityById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedPriority.DeleteStatus = 1;//changing job to postponed
                    await _priorityService.UpdatePriority(selectedPriority);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("ReadPriorityDetails");
            //return PartialView("ReadPriorityDetails");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedMaker(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedMaker = await _makerService.GetMakerById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedMaker.DeleteStatus = 1;//changing job to postponed
                    await _makerService.UpdateMaker(selectedMaker);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("ReadPriorityDetails");
            //return PartialView("ReadPriorityDetails");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSelectedMakerModel(string selectedIds)
        {
            await Task.FromResult(0);

            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {


                    var selectedMakerModel = await _makerService1.GetMakerModelById(strlist[i].Trim(new char[] { (char)39 }));

                    selectedMakerModel.DeleteStatus = 1;//changing job to postponed
                    await _makerService1.UpdateMakerModel(selectedMakerModel);
                }
            }

            //return Json(new { Result = true });
            return RedirectToAction("ReadPriorityDetails");
            //return PartialView("ReadPriorityDetails");
        }

    }
}
