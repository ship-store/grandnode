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
using Grand.Web.Areas.Maintenance.DomainModels;
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
    public class EquipmentMaster : BaseAdminController
    {
        private readonly IVesselService _vesselService;
        private readonly IHostingEnvironment env;
        private readonly IVesselViewModelService _vesselViewModelService;
        private readonly IEquipmentService _equipmentService;
        private readonly IJobplanService _jobplanService;
        private readonly ISparepartService _sparepartService;
        private readonly ISparepartViewModelService _sparepartViewModelService;
        private readonly IJobPlanViewModelService _jobPlanViewModelService;
        public EquipmentMaster(IJobPlanViewModelService _jobPlanViewModelService,ISparepartViewModelService _sparepartViewModelService,IJobplanService _jobplanService,IVesselViewModelService _vesselViewModelService, IEquipmentService _equipmentService, IVesselService _vesselService, IHostingEnvironment env,
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


        //list
        public IActionResult Index() => RedirectToAction("List2");

        public async Task<IActionResult> _General(EquipmentModel equipment)
        {

            return View(equipment);

        }

        //public async Task<IActionResult> SelectedEquipment(string equipmentName)
        //{
        //    HttpContext.Session.SetString("SelectedEquipmentName", equipmentName);

        //    var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
        //    var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
        //    var selectedEquipment = selectedEquipments.ToList().Where(x => x.Sub2_description == equipmentName || x.Sub1_description == equipmentName || x.Sub3_description == equipmentName || x.Sub4_description == equipmentName).First();

        //    var sparePart = spareparts.ToList().Where(x => x.EquipmentName == selectedEquipment.Sub1_description || x.EquipmentName == selectedEquipment.Sub2_description || x.EquipmentName == selectedEquipment.Sub3_description || x.EquipmentName == selectedEquipment.Sub4_description).First();




        //    ViewModel Data = new ViewModel();
        //    Data.SelectedEquipment = selectedEquipment;
        //    //Data.SelectedSparepart = sparePart;




        //    // return RedirectToAction("Edit", data);
        //    return RedirectToAction("Edit", "EquipmentMaster", new { equipmentName = equipmentName });
        //}
     

        public async Task<IActionResult> SelectedEquipments(string equipmentCode)
        {
             HttpContext.Session.SetString("eqpCode", equipmentCode);
            var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
            var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
            var selectedEquipment = selectedEquipments.ToList().Where(x => x.Sub2_number == equipmentCode || x.Sub1_number == equipmentCode || x.Sub3_number == equipmentCode || x.Sub4_number == equipmentCode).First();

            var sparePart = spareparts.ToList().Where(x => x.EquipmentCode == selectedEquipment.Sub1_number || x.EquipmentCode == selectedEquipment.Sub2_number || x.EquipmentCode == selectedEquipment.Sub3_number || x.EquipmentCode == selectedEquipment.Sub4_number).First();

            ViewModel Data = new ViewModel();
            Data.SelectedEquipment = selectedEquipment;
            //Data.SelectedSparepart = sparePart;

            // return RedirectToAction("Edit", data);
            return RedirectToAction("Edit2", "EquipmentMaster", new { equipmentCode = equipmentCode });
        }


        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command, VesselListModel model)
        {
            var vessels = await _vesselService.GetAllVessels(model.SearchName, command.Page - 1, command.PageSize, true);

            var gridModel = new DataSourceResult {
                Data = vessels.ToList()

            };
            return Json(gridModel);
        }

        //public async Task<IActionResult> List1()
        //{
        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> List1(DataSourceRequest command, EquipmentModel model)
        {
            var vessels = await _equipmentService.GetAllEquipment(model.SearchName, command.Page - 1, command.PageSize, true);

            var gridModel = new DataSourceResult {
                Data = vessels.ToList()
            };
            return Json(gridModel);

        }

        public async Task<IActionResult> Edit(string equipmentCode)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                //if (VesselName == null)
                //{
                //    return RedirectToAction("VesselList", "Vessel");
                //}
                //string VesselName = HttpContext.Session.GetString("VesselName").ToString();
                if (equipmentCode == null)
                {
                    var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 100, true);

                    var spareparts = await _sparepartService.GetAllSpareparts("", 0, 100, true);
                    List<Equipment> selectedEquipment = new List<Equipment>();
                    foreach (Equipment item in selectedEquipments)
                    {
                        if (item.Vessel.ToLower() == VesselName.ToLower())
                        {
                            selectedEquipment.Add(item);
                        }
                    }

                    List<int> sub = new List<int>();
                    foreach (Equipment item in selectedEquipment.Where(y => y.Sub1_number != null))
                    {
                        sub.Add(Int32.Parse(item.Sub1_number));
                    }

                    int min = 999;
                    foreach (int item in sub)
                    {
                        if (item < min)
                        {
                            min = item;
                        }
                    }
                    equipmentCode = min.ToString();
                    HttpContext.Session.SetString("eqcode", equipmentCode);
                    var selectedEquipments2 = await _equipmentService.GetAllEquipment("", 0, 500, true);
                    var selectedEquipment2 = selectedEquipments2.ToList().Where(src => src.Sub1_number == equipmentCode || src.Sub2_number == equipmentCode || src.Sub3_number == equipmentCode || src.Sub4_number == equipmentCode).First();

                    if (selectedEquipment2.Sub1_description != null)
                    {
                        HttpContext.Session.SetString("eqname", selectedEquipment2.Sub1_description);
                    }
                    if (selectedEquipment2.Sub2_description != null)
                    {
                        HttpContext.Session.SetString("eqname", selectedEquipment2.Sub2_description);
                    }
                    if (selectedEquipment2.Sub3_description != null)
                    {
                        HttpContext.Session.SetString("eqname", selectedEquipment2.Sub3_description);
                    }
                    if (selectedEquipment2.Sub4_description != null)
                    {
                        HttpContext.Session.SetString("eqname", selectedEquipment2.Sub4_description);
                    }
                    if (selectedEquipment2.Sub5_description != null)
                    {
                        HttpContext.Session.SetString("eqname", selectedEquipment2.Sub5_description);
                    }

                }
                //HttpContext.Session.SetString("Test", "Silpa");
                List<Equipment> hopeList = new List<Equipment>();
                List<EquipmentModel> equipmentModels = new List<EquipmentModel>();

                var equipments = await _equipmentService.GetAllEquipment("Equipment", 0, 500, true);

                foreach (Equipment item in equipments)
                {
                    if (item.Vessel.ToLower() == VesselName.ToLower())
                    {
                        hopeList.Add(item);
                    }
                }

                var x = hopeList;

                foreach (Equipment item2 in hopeList)
                {
                    equipmentModels.Add(new EquipmentModel() {
                        Id = item2.Id,
                        Vessel = item2.Vessel,
                        Department = item2.Department,
                        Equipment_Status = item2.Equipment_Status,
                        Sub1_number = item2.Sub1_number,
                        Sub1_description = item2.Sub1_description,
                        Sub2_number = item2.Sub2_number,
                        Sub2_description = item2.Sub2_description,
                        Sub3_number = item2.Sub3_number,
                        Sub3_description = item2.Sub3_description,
                        Sub4_number = item2.Sub4_number,
                        Sub5_description = item2.Sub5_description,
                        Safety_level = item2.Safety_level,
                        Maker = item2.Maker,
                        Model = item2.Model,
                        Drawing_no = item2.Drawing_no,
                        Remark = item2.Remark,
                        Type = item2.Type

                    });

                }

                ViewModel Data = new ViewModel();

                if (equipmentCode != null)
                {
                    var selectedEquipments1 = await _equipmentService.GetAllEquipment("", 0, 500, true);
                    var spareparts1 = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                    var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                    var selectedEquipment1 = selectedEquipments1.ToList().Where(src => src.Sub1_number == equipmentCode || src.Sub2_number == equipmentCode || src.Sub3_number == equipmentCode || src.Sub4_number == equipmentCode).First();

                    var sparePart = spareparts1.ToList().FindAll(y => y.EquipmentCode == selectedEquipment1.Sub1_number || y.EquipmentCode == selectedEquipment1.Sub2_number || y.EquipmentCode == selectedEquipment1.Sub3_number || y.EquipmentCode == selectedEquipment1.Sub4_number);
                    //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                    var jobplan = jobplans.ToList().FindAll(y => y.EquipmentCode == selectedEquipment1.Sub1_number || y.EquipmentCode == selectedEquipment1.Sub2_number || y.EquipmentCode == selectedEquipment1.Sub3_number || y.EquipmentCode == selectedEquipment1.Sub4_number);


                    Data.SelectedEquipment = selectedEquipment1;
                    Data.SelectedSparepart = sparePart;
                    Data.AllEquipments = hopeList;
                    Data.SelectedJobPlan = jobplan;
                    return View(Data);
                }
                Data.AllEquipments = hopeList;
                return View(Data);
            }

            catch (System.Exception)
            {

                return RedirectToAction("Success", "Register");
            }
        }


        public async Task<IActionResult> List2()
        {
            string VesselName = HttpContext.Session.GetString("VesselName").ToString();

            var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);

            var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
            List<Equipment> selectedEquipment = new List<Equipment>();
            foreach (Equipment item in selectedEquipments)
            {

                if (item.Vessel.ToLower() == VesselName.ToLower())
                {
                    selectedEquipment.Add(item);
                }

            }


            List<int> sub = new List<int>();
            foreach (Equipment item in selectedEquipment.Where(x => x.Sub1_number != null))
            {

                sub.Add(Int32.Parse(item.Sub1_number));
            }
            int min = 999;
            foreach (int item in sub)
            {
                if (item < min)
                {
                    min = item;
                }

            }
            //HttpContext.Session.SetString("Test", "Silpa");
            return RedirectToAction("SelectedEquipment", "EquipmentMaster", new { equipmentCode = min.ToString() });
            //return View();
        }


        public async Task<IActionResult> Edit2(string equipmentCode)
        {
            try
            {
                var VesselName = HttpContext.Session.GetString("VesselName").ToString();
                List<Equipment> hopeList = new List<Equipment>();
                List<EquipmentModel> equipmentModels = new List<EquipmentModel>();

                var equipments = await _equipmentService.GetAllEquipment("Equipment", 0, 500, true);

                foreach (Equipment item in equipments)
                {
                    if (item.Vessel.ToLower() == VesselName.ToLower())
                    {
                        hopeList.Add(item);
                    }
                }

                var x = hopeList;

                foreach (Equipment item2 in hopeList)
                {
                    equipmentModels.Add(new EquipmentModel() {
                        Id = item2.Id,
                        Vessel = item2.Vessel,
                        Department = item2.Department,
                        Equipment_Status = item2.Equipment_Status,
                        Sub1_number = item2.Sub1_number,
                        Sub1_description = item2.Sub1_description,
                        Sub2_number = item2.Sub2_number,
                        Sub2_description = item2.Sub2_description,
                        Sub3_number = item2.Sub3_number,
                        Sub3_description = item2.Sub3_description,
                        Sub4_number = item2.Sub4_number,
                        Sub5_description = item2.Sub5_description,
                        Safety_level = item2.Safety_level,
                        Maker = item2.Maker,
                        Model = item2.Model,
                        Drawing_no = item2.Drawing_no,
                        Remark = item2.Remark,
                        Type = item2.Type
                    });

                }

                ViewModel Data = new ViewModel();


                if (equipmentCode != null)
                {
                    var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
                    var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                    var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                    var selectedEquipment = selectedEquipments.ToList().Where(src => src.Sub1_number == equipmentCode || src.Sub2_number == equipmentCode || src.Sub3_number == equipmentCode || src.Sub4_number == equipmentCode).First();

                    var sparePart = spareparts.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);
                    //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                    var jobplan = jobplans.ToList().FindAll(y => y.EquipmentCode == selectedEquipment.Sub1_number || y.EquipmentCode == selectedEquipment.Sub2_number || y.EquipmentCode == selectedEquipment.Sub3_number || y.EquipmentCode == selectedEquipment.Sub4_number);

                    Data.SelectedEquipment = selectedEquipment;
                    Data.SelectedSparepart = sparePart;
                    Data.AllEquipments = hopeList;
                    Data.SelectedJobPlan = jobplan;
                    return View(Data);
                }
                Data.AllEquipments = hopeList;
                return View(Data);
            }
            catch (System.Exception)
            {

                return RedirectToAction("Success", "Register");
            }
        }

       
        [HttpGet]
        public async Task<IActionResult> EditItem(VesselForDisplay vesselForDisplay, string id)
        {
            id = vesselForDisplay.VesselID;
            var vessel = await _vesselService.GetVesselById(id);
            vessel.Vessel_type = vesselForDisplay.Vessel_type;
            vessel.Vessel_name = vesselForDisplay.Vessel_name;

            vessel.IMO = vesselForDisplay.IMO;
            vessel.Class = vesselForDisplay.Class;
            vessel.Hull_no = vesselForDisplay.Hull_no;
            vessel.Shipyard = vesselForDisplay.Shipyard;
            vessel.Flag = vesselForDisplay.Flag;
            vessel.Main_Engine = vesselForDisplay.Main_Engine;
            vessel.Auxiliary_Engine = vesselForDisplay.Auxiliary_Engine;

            await _vesselService.UpdateVessel(vessel);
            return RedirectToAction("List");
        }


        //Edit Item2
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

        [HttpGet]
        public async Task<IActionResult> DeleteSelected(string selectedIds)
        {
            string[] strlist = selectedIds.Split(",");

            var SelectedList = strlist.ToList();
            if (selectedIds != null)
            {
                for (int i = 0; i < strlist.Length; i++)
                {
                    var vessel = await _vesselService.GetVesselById(strlist[i].Trim(new char[] { (char)39 }));
                    await _vesselViewModelService.DeleteVessel(vessel);
                }
            }

            return Json(new { Result = true });
        }

        [HttpPost]
        public ActionResult Remote_Data_Binding()
        {
            return View();
        }


        public async Task<IActionResult> ViewJobplan(string id)
        {

            List<Jobplan> hopeList = new List<Jobplan>();
            List<JobplanModel> equipmentModels = new List<JobplanModel>();

            var equipment = await _equipmentService.GetEquipmentById(id);
            var jobplans = await _jobplanService.GetAllJobpan("Jobplan", 0, 500, true);

            foreach (Jobplan item in jobplans)
            {

                if (item.EquipmentCode == equipment.Sub1_number)
                {
                    hopeList.Add(item);
                }
                else if (item.EquipmentCode == equipment.Sub2_number)
                {
                    hopeList.Add(item);
                }
                else if (item.EquipmentCode == equipment.Sub3_number)
                {
                    hopeList.Add(item);
                }
                else if (item.EquipmentCode == equipment.Sub4_number)
                {
                    hopeList.Add(item);
                }
                else if (item.EquipmentCode == equipment.Sub5_number)
                {
                    hopeList.Add(item);
                }

            }

            var x = hopeList;

            foreach (Jobplan item2 in hopeList)
            {
                equipmentModels.Add(new JobplanModel() {

                    Vessel = item2.Vessel,
                    Department = item2.Department,
                    EquipmentCode = item2.EquipmentCode,
                    EquipmentName = item2.EquipmentName,
                    JobTitle = item2.JobTitle,
                    JobDescription = item2.JobDescription,
                    CalFrequency = item2.CalFrequency,
                    FrequencyType = item2.FrequencyType,

                    Priority = item2.Priority,
                    Rank = item2.Rank,
                    AssignedTo = item2.AssignedTo,
                    LAST_DONE_DATE = item2.LAST_DONE_DATE,
                    NEXT_DUE_DATE = item2.NEXT_DUE_DATE,
                    Job_Type = item2.Job_Type,
                    Maintenance_Type = item2.Maintenance_Type,

                });

            }

            return View(equipmentModels);
        }


        //Add New JobPlan

        [HttpGet]
        public async Task<IActionResult> AddJobPlan()
        {
              var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            List<Equipment> hopeList = new List<Equipment>();
            List<EquipmentModel> equipmentModels = new List<EquipmentModel>();

            var equipments = await _equipmentService.GetAllEquipment("Equipment", 0, 500, true);
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
            var dt = Convert.ToDateTime(addNewJobPlan.LAST_DONE_DATE).ToString("yyyy-MM-dd");
            addNewJobPlan.LAST_DONE_DATE = dt;
            await _jobPlanViewModelService.PrepareJobplanModel(addNewJobPlan, "Jobplan", true);
            return RedirectToAction("Edit", "EquipmentMaster");
        }



        //Add SparePART


        [HttpGet]
        public async Task<IActionResult> AddSparepart(ViewModel equipment)
        {
            var model = await Task.FromResult<object>(null);

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

        public async Task<IActionResult> SelectedEquipment(string equipmentCode)
        {
            HttpContext.Session.SetString("SelectedEquipmentCode", equipmentCode);

            var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
            var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
            var selectedEquipment = selectedEquipments.ToList().Where(x => x.Sub2_number == equipmentCode || x.Sub1_number == equipmentCode || x.Sub3_number == equipmentCode || x.Sub4_number == equipmentCode).First();

            var sparePart = spareparts.ToList().Where(x => x.EquipmentCode == selectedEquipment.Sub1_number || x.EquipmentCode == selectedEquipment.Sub2_number || x.EquipmentCode == selectedEquipment.Sub3_number || x.EquipmentCode == selectedEquipment.Sub4_number).First();
            if (selectedEquipment.Sub1_number != null)
            {
                HttpContext.Session.SetString("eqcode", selectedEquipment.Sub1_number);
            }
            if (selectedEquipment.Sub2_number != null)
            {
                HttpContext.Session.SetString("eqcode", selectedEquipment.Sub2_number);
            }
            if (selectedEquipment.Sub3_number != null)
            {
                HttpContext.Session.SetString("eqcode", selectedEquipment.Sub3_number);
            }
            if (selectedEquipment.Sub4_number != null)
            {
                HttpContext.Session.SetString("eqcode", selectedEquipment.Sub4_number);
            }
            if (selectedEquipment.Sub5_number != null)
            {
                HttpContext.Session.SetString("eqcode", selectedEquipment.Sub5_number);
            }
            if (selectedEquipment.Sub1_description != null)
            {
                HttpContext.Session.SetString("eqname", selectedEquipment.Sub1_description);
            }
            if (selectedEquipment.Sub2_description != null)
            {
                HttpContext.Session.SetString("eqname", selectedEquipment.Sub2_description);
            }
            if (selectedEquipment.Sub3_description != null)
            {
                HttpContext.Session.SetString("eqname", selectedEquipment.Sub3_description);
            }
            if (selectedEquipment.Sub4_description != null)
            {
                HttpContext.Session.SetString("eqname", selectedEquipment.Sub4_description);
            }
            if (selectedEquipment.Sub5_description != null)
            {
                HttpContext.Session.SetString("eqname", selectedEquipment.Sub5_description);
            }
            ViewModel Data = new ViewModel();
            Data.SelectedEquipment = selectedEquipment;
            //Data.SelectedSparepart = sparePart;

            // return RedirectToAction("Edit", data);
            return RedirectToAction("Edit", "EquipmentMaster", new { equipmentCode = equipmentCode });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadJobPlan(DataSourceRequest command, JobplanListModel model)
        {
            string selectedEquipmentCode=null;
            DataSourceResult gridModel=null;
            List<JobPlanForDisplay> jp = new List<JobPlanForDisplay>();

            try
            {
                selectedEquipmentCode = HttpContext.Session.GetString("SelectedEquipmentCode").ToString();


                //  var VesselName = HttpContext.Session.GetString("VesselName").ToString();

                var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
                var spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                var selectedEquipment = selectedEquipments.ToList().Where(src => src.Sub2_number == selectedEquipmentCode || src.Sub1_number == selectedEquipmentCode || src.Sub3_number == selectedEquipmentCode || src.Sub4_number == selectedEquipmentCode).First();

                var sparePart = spareparts.ToList().FindAll(y => y.EquipmentName == selectedEquipment.Sub1_description || y.EquipmentName == selectedEquipment.Sub2_description || y.EquipmentName == selectedEquipment.Sub3_description || y.EquipmentName == selectedEquipment.Sub4_description);
                //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                var jobplan = jobplans.ToList().FindAll(y => y.EquipmentName == selectedEquipment.Sub1_description || y.EquipmentName == selectedEquipment.Sub2_description || y.EquipmentName == selectedEquipment.Sub3_description || y.EquipmentName == selectedEquipment.Sub4_description);

                
                foreach (var item in jobplan)
                {
                    jp.Add(new JobPlanForDisplay() 
                    {
                        EquipmentName = item.EquipmentName,
                        EquipmentCode = item.EquipmentCode,
                        Vessel = item.Vessel,
                        JobTitle = item.JobTitle,
                        JobDescription = item.JobDescription,
                        CalFrequency = item.CalFrequency,
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
                    });
                }
            }
            catch (Exception)
            {
                gridModel = new DataSourceResult { Data=jp};
                return Json(gridModel);
            }
            gridModel = new DataSourceResult { Data = jp.ToList().Where(x => x.JobStatus == 0)};
            return Json(gridModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadSpareParts(DataSourceRequest command)
        {
            string selectedEquipmentCode = null;
            DataSourceResult gridModel = null;
            var sparePart = new List<Sparepart>{ };
            try
            {
              selectedEquipmentCode = HttpContext.Session.GetString("SelectedEquipmentCode").ToString();
                // var VesselName = HttpContext.Session.GetString("VesselName").ToString();

                var selectedEquipments = await _equipmentService.GetAllEquipment("", 0, 500, true);
                var  spareparts = await _sparepartService.GetAllSpareparts("", 0, 500, true);
                var jobplans = await _jobplanService.GetAllJobplan("", 0, 500, true);
                var selectedEquipment = selectedEquipments.ToList().Where(src => src.Sub2_number == selectedEquipmentCode || src.Sub1_number == selectedEquipmentCode || src.Sub3_number == selectedEquipmentCode || src.Sub4_number == selectedEquipmentCode).First();

                sparePart = spareparts.ToList().FindAll(y => y.EquipmentName == selectedEquipment.Sub1_description || y.EquipmentName == selectedEquipment.Sub2_description || y.EquipmentName == selectedEquipment.Sub3_description || y.EquipmentName == selectedEquipment.Sub4_description);
                //var jobplan = jobplans.ToList().Where(y => y.EquipmentName.ToLower() == selectedEquipment.Sub1_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub2_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub3_description.ToLower() || y.EquipmentName.ToLower() == selectedEquipment.Sub4_description.ToLower()).First();
                var jobplan = jobplans.ToList().FindAll(y => y.EquipmentName == selectedEquipment.Sub1_description || y.EquipmentName == selectedEquipment.Sub2_description || y.EquipmentName == selectedEquipment.Sub3_description || y.EquipmentName == selectedEquipment.Sub4_description);

            }
            catch (Exception ex)
            {

                gridModel = new DataSourceResult { Data = sparePart };
                return Json(gridModel);


               
            }

            gridModel = new DataSourceResult { Data = sparePart.ToList() };
            return Json(gridModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateJobPlan(DataSourceRequest command, Jobplan models)
        {

            await Task.FromResult(0);
            var lastDone = Convert.ToDateTime(models.LAST_DONE_DATE);
            var selectedJobPlan = await _jobplanService.GetJobPlanById(models.Id);
            int days = 0;
            if (models.FrequencyType.ToLower() == "month")
            {
                days = Convert.ToInt32(models.CalFrequency) * 30;
            }
            else if (models.FrequencyType.ToLower() == "week")
            {
                days = Convert.ToInt32(models.CalFrequency) * 7;
            }

            selectedJobPlan.NEXT_DUE_DATE = lastDone.AddDays(days).ToString("M/d/yyyy hh:mm:ss tt");
          
            selectedJobPlan.LAST_DONE_DATE = lastDone.ToString("M/d/yyyy hh:mm:ss tt");
            await _jobplanService.UpdateJobPlan(selectedJobPlan);

            return Json(models);


        }

        [HttpPost]
        public async Task<JsonResult> UpdateSpareParts(DataSourceRequest command, List<Sparepart> models)
        {
            foreach (var item in models)
            {
                var sparepart = await _sparepartService.GetSparepartById(item.Id);
                sparepart.EquipmentName = item.EquipmentName;
                sparepart.EquipmentCode = item.EquipmentCode;
                sparepart.SPAR_PARTS_DESCRIPTION = item.SPAR_PARTS_DESCRIPTION;
                sparepart.PART_NUMBER = item.PART_NUMBER;
                sparepart.DRAWING_NO = item.DRAWING_NO;
                sparepart.SPECIFICATION = item.SPECIFICATION;
                sparepart.POSITION_NUMBER = item.POSITION_NUMBER;

                await _sparepartService.UpdateSparePart(sparepart);
            }
            return Json(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>EditJobplan(Jobplan model, int jobOrder, string lastdone)
        {
            var joborder = jobOrder;
            var job = await _jobplanService.GetAllJobplan("", 0, 500, true);
            var jobplan = job.ToList().FindAll(y => y.JobOrder== joborder);

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


            return RedirectToAction("Edit");
        }

    }
}
