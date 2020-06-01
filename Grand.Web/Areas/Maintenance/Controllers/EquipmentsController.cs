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

        private readonly IEquipmentTypeViewModelService _equipmentTypeViewModelService;
        private readonly IVesselService _vesselService;
        private readonly IHostingEnvironment env;
        private readonly IVesselViewModelService _vesselViewModelService;
        private readonly IEquipmentService _equipmentService;
        private readonly IJobplanService _jobplanService;
        private readonly ISparepartService _sparepartService;
        private readonly ISparepartViewModelService _sparepartViewModelService;
        private readonly IJobPlanViewModelService _jobPlanViewModelService;
        public EquipmentsController(IJobPlanViewModelService _jobPlanViewModelService, ISparepartViewModelService _sparepartViewModelService, IJobplanService _jobplanService, IVesselViewModelService _vesselViewModelService, IEquipmentService _equipmentService, IVesselService _vesselService, IHostingEnvironment env,
            ISparepartService _sparepartService,
            IEquipmentTypeViewModelService _equipmentTypeViewModelService
            )
        {

            this._vesselViewModelService = _vesselViewModelService;
            this._vesselService = _vesselService;
            this._equipmentService = _equipmentService;
            this.env = env;
            this._jobplanService = _jobplanService;
            this._sparepartViewModelService = _sparepartViewModelService;
            this._sparepartService = _sparepartService;
            this._jobPlanViewModelService = _jobPlanViewModelService;
            this._equipmentTypeViewModelService = _equipmentTypeViewModelService;
        }


        public IActionResult Index() => RedirectToAction("List");

        [HttpGet]
        public async  Task<IActionResult> AddEquipment(string equipment_Code, string sub_number)
        {

            var EquipmentTypeList=await _equipmentTypeViewModelService.GetAllEquipmentTypeAsList("");
            ViewBag.subNumber = sub_number;
            ViewBag.equipment_Code = equipment_Code;
            ViewBag.VesselName = HttpContext.Session.GetString("VesselName").ToString();
            return View(EquipmentTypeList.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddEquipments(string equipmentCode,string sub_number)
        {

            await Task.FromResult(0);
          return Json(Url.Action("AddEquipment", "Equipment", new { equipment_Code = equipmentCode , subNumber= sub_number }));
        }

        [HttpPost]
        public IActionResult AddEquipment(Equipment newEquipment)
        {

            //

            if (newEquipment.Sub1_number.Length == 3)//if(sub_number==631)
            {

                newEquipment.Sub1_number = newEquipment.Sub1_number;
                newEquipment.Sub1_description = newEquipment.Sub1_description;

            }
            else if (newEquipment.Sub1_number.Length == 6)//if(sub_number==631.01)
            {
                newEquipment.Sub2_number = newEquipment.Sub1_number;
                newEquipment.Sub2_description = newEquipment.Sub1_description;

            }
            else if (newEquipment.Sub1_number.Length == 8)//if(sub_number==631.01.01)
            {
                newEquipment.Sub3_number = newEquipment.Sub1_number;
                newEquipment.Sub3_description = newEquipment.Sub1_description;
            }
            else if (newEquipment.Sub1_number.Length == 10)//if(sub_number==631.01.01.01)
            {
                newEquipment.Sub4_number = newEquipment.Sub1_number;
                newEquipment.Sub4_description = newEquipment.Sub1_description;
            }
            else if (newEquipment.Sub1_number.Length == 12)//if(sub_number==631.01.01.01)
            {
                newEquipment.Sub5_number = newEquipment.Sub1_number;
                newEquipment.Sub5_description = newEquipment.Sub1_description;
            }

            _equipmentService.InsertEquipment(newEquipment);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> List(string equipmentCode)
        {
            try
            {
                //Load tree first
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (VesselName != null)
                {
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
        public async Task<IActionResult> GeneralEdit(Equipment vmodel, string id, string code,
            string remark, string safety, 
            string maker, string model,
            string eqtype, string drawno, 
            string dept, string location, 
            string eqstatus, string type)
        {
            var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);

            var selectedEquipment = selectedEquipments.ToList()
                .Where(src => src.Sub1_number == code || 
                src.Sub2_number == code || src.Sub3_number == code || 
                src.Sub4_number == code).First();

            selectedEquipment.Remark = remark;
            selectedEquipment.Safety_level = safety;
            selectedEquipment.Maker = maker;
            selectedEquipment.Model = model;
            selectedEquipment.Equipment_type = type;
            selectedEquipment.Equipment_Status = eqstatus;
            selectedEquipment.Drawing_no = drawno;
            selectedEquipment.Department = dept;
            selectedEquipment.Location = location;
            selectedEquipment.Type = type;

            await _equipmentService.UpdateEquipment(selectedEquipment);
            return RedirectToAction("List");

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

                if (item.EquipmentName == addNewJobPlan.EquipmentName && item.JobOrder == max)
                {
                    prReading = item.PreviousReading;
                }
            }

            //foreach (Jobplan item2 in jobplans.Where(x => x.EquipmentName == addNewJobPlan.EquipmentName && x.JobOrder == max))
            //{
            //    prReading = item2.PreviousReading;
            //}

            addNewJobPlan.PreviousReading = prReading;
            var dueRhsFreq = Convert.ToInt32(addNewJobPlan.RunFrequency);
            int dueRhs = prReading + dueRhsFreq;

            addNewJobPlan.JobOrder = max + 1;
            var dt = Convert.ToDateTime(addNewJobPlan.LAST_DONE_DATE).ToString("yyyy-MM-dd");
            addNewJobPlan.LAST_DONE_DATE = dt;
            addNewJobPlan.Vessel = VesselName;

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
                        var hrsfrq = Convert.ToInt32(item.RunFrequency);
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
                            PreviousReading = item.PreviousReading,
                            LastReading = item.LastReading,
                            DueRhs = item.PreviousReading + hrsfrq,
                        }) ;
                    }
                }

                gridModel = new DataSourceResult { Data = jp.ToList() };
                return Json(gridModel);
            }
            catch (System.Exception)
            {
                //gridModel = new DataSourceResult { Data = jp };
                //return Json(gridModel);
                return RedirectToAction("Success", "Register");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadSpareParts(DataSourceRequest command, string selectedEquipmentCode)
        {
            DataSourceResult gridModel = null;
              List<SparepartForDisplay> sp = new List<SparepartForDisplay>();
          

            var sparePart = new List<Sparepart> { };
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
                var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                var selectedEquipment = selectedEquipments.ToList().Where(src => src.Sub2_number == selectedEquipmentCode || src.Sub1_number == selectedEquipmentCode || src.Sub3_number == selectedEquipmentCode || src.Sub4_number == selectedEquipmentCode).First();
                 sparePart = spareparts.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);
                //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                var jobplan = jobplans.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);

                foreach (var item in sparePart)
                {
                    if (item.Vessel.ToLower() == VesselName.ToLower())
                    {
                        //gridModel = new DataSourceResult { Data = sparePart.ToList().Where(x=> x.EquipmentCode == selectedEquipment.Sub1_number) };
                        sp.Add(new SparepartForDisplay() {
                            EquipmentName = item.EquipmentName,
                            EquipmentCode = item.EquipmentCode,
                            SPAR_PARTS_DESCRIPTION = item.SPAR_PARTS_DESCRIPTION,
                            PART_NUMBER=item.PART_NUMBER,
                            DRAWING_NO=item.DRAWING_NO,
                            SPECIFICATION=item.SPECIFICATION,
                            POSITION_NUMBER=item.POSITION_NUMBER
                        });
                       
                    }
                    
                }
                gridModel = new DataSourceResult { Data = sp.ToList() };
                return Json(gridModel);


            }
            catch (System.Exception)
            {
                return RedirectToAction("Success", "Register");
                //gridModel = new DataSourceResult { Data = sparePart };
                //return Json(gridModel);
            }           
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
        public async Task<ActionResult> EditJobplan(Jobplan model, int jobOrder2, string lastdone)
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
        [HttpGet]
        public async Task<IActionResult> AddSparepart(ViewModel equipment)
        {
            var model = await Task.FromResult<object>(null);
            var eqcodes = HttpContext.Session.GetString("eqcode").ToString();
            var eqnames = HttpContext.Session.GetString("eqname").ToString();
            ViewBag.code = eqcodes;
            ViewBag.name = eqnames;
            return View(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSparepartDetails(SparepartModel sparepart, string id, ViewModel equipment)
        {

            var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);


            List<Equipment> selectedEquipment = new List<Equipment>();

            //string VesselName = HttpContext.Session.GetString("VesselName").ToString();
            //string Sub1_description = HttpContext.Session.GetString("Sub1_description").ToString();
            //string Sub2_description = HttpContext.Session.GetString("Sub2_description").ToString();
            //string Sub3_description = HttpContext.Session.GetString("Sub3_description").ToString();
            await _sparepartViewModelService.PrepareSparepartModel(sparepart, "Sparepart", true);

            return RedirectToAction("Edit", "EquipmentMaster");
        }
        public async Task<IActionResult> EditItem2(ViewModel vmodel, string id)
        {
            id = vmodel.SelectedEquipment.Id;
            var equipment = await _equipmentService.GetEquipmentById(id);
            equipment.Sub1_number = vmodel.SelectedEquipment.Sub1_number;
            equipment.Sub1_description = vmodel.SelectedEquipment.Sub1_description;
            equipment.Sub2_number = vmodel.SelectedEquipment.Sub2_number;
            equipment.Sub2_description = vmodel.SelectedEquipment.Sub2_description;
            equipment.Sub3_number = vmodel.SelectedEquipment.Sub3_number;
            equipment.Sub3_description = vmodel.SelectedEquipment.Sub3_description;
            equipment.Sub4_number = vmodel.SelectedEquipment.Sub4_number;
            equipment.Sub4_description = vmodel.SelectedEquipment.Sub4_description;
            equipment.Sub5_number = vmodel.SelectedEquipment.Sub5_number;
            equipment.Sub5_description = vmodel.SelectedEquipment.Sub5_description;
            equipment.Safety_level = vmodel.SelectedEquipment.Safety_level;
            equipment.Maker = vmodel.SelectedEquipment.Maker;
            equipment.Model = vmodel.SelectedEquipment.Model;
            equipment.Equipment_type = vmodel.SelectedEquipment.Equipment_type;
            equipment.Drawing_no = vmodel.SelectedEquipment.Drawing_no;
            equipment.Department = vmodel.SelectedEquipment.Department;
            equipment.Location = vmodel.SelectedEquipment.Location;
            equipment.Equipment_Status = vmodel.SelectedEquipment.Equipment_Status;
            equipment.Remark = vmodel.SelectedEquipment.Remark;
            equipment.Vessel = vmodel.SelectedEquipment.Vessel;
            equipment.Type = vmodel.SelectedEquipment.Type;

            await _equipmentService.UpdateEquipment(equipment);
            return RedirectToAction("Edit");

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
                var prReading = item.PreviousReading + currentrhs;
                item.PreviousReading = prReading;


                await _jobplanService.UpdateJobPlan(item);
            }

            return RedirectToAction("List");
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

    }
}
