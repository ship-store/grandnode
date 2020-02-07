using Grand.Core.Domain.Equipment;
using Grand.Core.Domain.Jobplan;
using Grand.Core.Domain.Vessel;
using Grand.Framework.Kendoui;
using Grand.Services.Equipments;
using Grand.Services.Jobplan;
using Grand.Services.Vendors;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Admin.Models.Vendors;
using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using Grand.Services.Spareparts;
using Grand.Core.Domain.Sparepart;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class EquipmentsController : BaseAdminController
    {


        private readonly IVesselService _vesselService;
        private readonly IHostingEnvironment env;
        private readonly IVesselViewModelService _vesselViewModelService;
        private readonly IEquipmentService _equipmentService;
        private readonly IJobplanService _jobplanService;
        private readonly ISparepartService _sparepartService;
        private readonly ISparepartViewModelService _sparepartViewModelService;
        private readonly IJobPlanViewModelService _jobPlanViewModelService;
        public EquipmentsController(IJobPlanViewModelService _jobPlanViewModelService, ISparepartViewModelService _sparepartViewModelService, IJobplanService _jobplanService, IVesselViewModelService _vesselViewModelService, IEquipmentService _equipmentService, IVesselService _vesselService, IHostingEnvironment env,
            ISparepartService _sparepartService)
        {

            this._vesselViewModelService = _vesselViewModelService;
            this._vesselService = _vesselService;
            this._equipmentService = _equipmentService;
            this.env = env;
            this._jobplanService = _jobplanService;
            this._sparepartViewModelService = _sparepartViewModelService;
            this._sparepartService = _sparepartService;
            this._jobPlanViewModelService = _jobPlanViewModelService;
        }


        public IActionResult Index() => RedirectToAction("List");

        public async Task<IActionResult> List(string equipmentCode)
        {
            //Load tree first
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            List<Equipment> equipmentList = new List<Equipment>();
            List<EquipmentModel> equipmentModels = new List<EquipmentModel>();
            var equipments = await _equipmentService.GetAllEquipment("Equipment", 0, 500, true);
            List<Equipment> selectedEquipment = new List<Equipment>();
            foreach (Equipment item in equipments)
            {
                if (item.Vessel.ToLower() == VesselName.ToLower())
                {
                    selectedEquipment.Add(item);
                }
            }
            ViewModel vm = new ViewModel();
            vm.AllEquipments = selectedEquipment;
           return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> NewJobPlan()
        {
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            List<Equipment> hopeList = new List<Equipment>();
            List<EquipmentModel> equipmentModels = new List<EquipmentModel>();

            var equipments = await _equipmentService.GetAllEquipment("Equipment", 0, 500, true);
            var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);

            int max = 9999;
            foreach (Jobplan item in jobplans)
            {

                if (item.JobOrder > max)
                {
                    max = item.JobOrder;
                }
            }
            int order = max + 1;
            ViewBag.joborder = VesselName + order;

            var eqcodes = HttpContext.Session.GetString("eqcode").ToString();
            var eqnames = HttpContext.Session.GetString("eqname").ToString();
            var model = await Task.FromResult<object>(null);
            ViewBag.code = eqcodes;
            ViewBag.name = eqnames;
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobPlanDetails(JobplanModel addNewJobPlan)
        {
            var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
            int max = 9999;
            int prReading = 0;

            string VesselName = HttpContext.Session.GetString("VesselName").ToString();
            foreach (Jobplan item in jobplans)
            {
                if (item.JobOrder > max)
                {
                    max = item.JobOrder;
                }
            }

            foreach (Jobplan item2 in jobplans.Where(x => x.EquipmentName == addNewJobPlan.EquipmentName && x.JobOrder == max))
            {
                prReading = item2.InitialReading;   
            }

            addNewJobPlan.PreviousReading = prReading;
            var dueRhsFreq = Convert.ToInt32(addNewJobPlan.RunFrequency);
            int dueRhs = prReading + dueRhsFreq;
            addNewJobPlan.JobOrder = max + 1;
            var dt = Convert.ToDateTime(addNewJobPlan.LAST_DONE_DATE).ToString("yyyy-MM-dd");
            
            addNewJobPlan.LAST_DONE_DATE = dt;
            addNewJobPlan.Vessel = VesselName;
            addNewJobPlan.DueRhs = dueRhs;

            await _jobPlanViewModelService.PrepareJobplanModel(addNewJobPlan, "Jobplan", true);
            return RedirectToAction("List", "Equipments");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadJobPlan(DataSourceRequest command, JobplanListModel model, string selectedEquipmentCode)
        {
            DataSourceResult gridModel = null;
            List<JobPlanForDisplay> jp = new List<JobPlanForDisplay>();
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
                var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                var selectedEquipment = selectedEquipments.ToList().Where(src => src.Sub2_number == selectedEquipmentCode || src.Sub1_number == selectedEquipmentCode || src.Sub3_number == selectedEquipmentCode || src.Sub4_number == selectedEquipmentCode).First();

                var sparePart = spareparts.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);
                //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                var jobplan = jobplans.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);
               

                foreach (var item in jobplan)
                {
                    
                    if (item.Vessel.ToLower() == VesselName.ToLower())
                    {
                        jp.Add(new JobPlanForDisplay() {
                            EquipmentName = item.EquipmentName,
                            EquipmentCode = item.EquipmentCode,
                            Vessel = item.Vessel,
                            JobTitle = item.JobTitle,
                            JobDescription = item.JobDescription,
                            CalFrequency = item.CalFrequency,
                            RunFrequency = item.RunFrequency,
                            FrequencyType = item.FrequencyType,
                            Department = item.Department,
                            Priority = item.Priority,
                            Rank = item.Rank,
                            AssignedTo = item.AssignedTo,
                            Job_Type = item.Job_Type,
                            JobStatus = item.JobStatus,
                            JobOrder = item.JobOrder,
                            Maintenance_Type = item.Maintenance_Type,
                            LAST_DONE_DATE = Convert.ToDateTime(item.LAST_DONE_DATE),
                            NEXT_DUE_DATE = Convert.ToDateTime(item.NEXT_DUE_DATE),
                            InitialReading = item.InitialReading,
                            LastReading = item.LastReading,
                            PreviousReading = item.PreviousReading,
                            DueRhs = item.DueRhs
                        }); ;
                    }
                }
            }
            catch (Exception)
            {
                gridModel = new DataSourceResult { Data = jp };
                return Json(gridModel);
            }
            gridModel = new DataSourceResult { Data = jp.ToList() };
            return Json(gridModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadSpareParts(DataSourceRequest command, string selectedEquipmentCode)
        {
            DataSourceResult gridModel = null;
            var sparePart = new List<Sparepart> { };
            try
            {
                var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
                var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                var selectedEquipment = selectedEquipments.ToList().Where(src => src.Sub2_number == selectedEquipmentCode || src.Sub1_number == selectedEquipmentCode || src.Sub3_number == selectedEquipmentCode || src.Sub4_number == selectedEquipmentCode).First();
                 sparePart = spareparts.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);
                //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                var jobplan = jobplans.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);
            }
            catch (Exception ex)
            {

                gridModel = new DataSourceResult { Data = sparePart };
                return Json(gridModel);
            }

            gridModel = new DataSourceResult { Data = sparePart.ToList() };
            return Json(gridModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipmentDetails(string equipmentCode)
        {

            var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
            var SelectedEquipment = selectedEquipments.ToList().Where(src => src.Sub2_number == equipmentCode || src.Sub1_number == equipmentCode || src.Sub3_number == equipmentCode || src.Sub4_number == equipmentCode).First();
            ViewModel vm = new ViewModel();
            vm.SelectedEquipment = SelectedEquipment;
            if (SelectedEquipment.Sub1_number != null)
            {
                HttpContext.Session.SetString("eqcode", SelectedEquipment.Sub1_number);
            }
            if (SelectedEquipment.Sub2_number != null)
            {
                HttpContext.Session.SetString("eqcode", SelectedEquipment.Sub2_number);
            }
            if (SelectedEquipment.Sub3_number != null)
            {
                HttpContext.Session.SetString("eqcode", SelectedEquipment.Sub3_number);
            }
            if (SelectedEquipment.Sub4_number != null)
            {
                HttpContext.Session.SetString("eqcode", SelectedEquipment.Sub4_number);
            }
            if (SelectedEquipment.Sub5_number != null)
            {
                HttpContext.Session.SetString("eqcode", SelectedEquipment.Sub5_number);
            }
            if (SelectedEquipment.Sub1_description != null)
            {
                HttpContext.Session.SetString("eqname", SelectedEquipment.Sub1_description);
            }
            if (SelectedEquipment.Sub2_description != null)
            {
                HttpContext.Session.SetString("eqname", SelectedEquipment.Sub2_description);
            }
            if (SelectedEquipment.Sub3_description != null)
            {
                HttpContext.Session.SetString("eqname", SelectedEquipment.Sub3_description);
            }
            if (SelectedEquipment.Sub4_description != null)
            {
                HttpContext.Session.SetString("eqname", SelectedEquipment.Sub4_description);
            }
            if (SelectedEquipment.Sub5_description != null)
            {
                HttpContext.Session.SetString("eqname", SelectedEquipment.Sub5_description);
            }

            var jsnList = Newtonsoft.Json.JsonConvert.SerializeObject(vm.SelectedEquipment);
          
            return Json(jsnList);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditJobplanCalendar(Jobplan model, int jobOrder2, string lastdone)
        {
            var joborder = jobOrder2;
            var job = await _jobplanService.GetAllJobplan("", 0, 500, true);
            var jobplan = job.ToList().FindAll(y => y.JobOrder == joborder);

            var doneDate = Convert.ToDateTime(lastdone);
            var lastDDate = doneDate.ToString("yyyy-MM-dd");

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


                await _jobplanService.UpdateJobPlan(item);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditJobplanRhs(Jobplan model, int jobOrder3, string lastdone, int currentRhs)
        {
            var joborder = jobOrder3;
            var currentrhs = currentRhs;

            var job = await _jobplanService.GetAllJobplan("", 0, 500, true);
            var jobplan = job.ToList().FindAll(y => y.JobOrder == joborder);

            var doneDate = Convert.ToDateTime(lastdone);
            var lastDDate = doneDate.ToString("yyyy-MM-dd");

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
                item.LastReading = currentrhs;


                await _jobplanService.UpdateJobPlan(item);
            }

            return RedirectToAction("List");
        }


    }
}
