using Grand.Core.Domain.Equipment;
using Grand.Core.Domain.Jobplan;
using Grand.Core.Domain.Vessel;
using Grand.Framework.Kendoui;
using Grand.Services.Equipments;
using Grand.Services.Jobplan;
using Grand.Services.Vendors;
using Grand.Services.Vessel;


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

        public EquipmentMaster(IJobplanService _jobplanService,IVesselViewModelService _vesselViewModelService, IEquipmentService _equipmentService, IVesselService _vesselService, IHostingEnvironment env)
        {

            this._vesselViewModelService = _vesselViewModelService;
            this._vesselService = _vesselService;
            this._equipmentService = _equipmentService;
            this.env = env;
            this._jobplanService = _jobplanService;


        }

        //list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> List()
        {

            return View();

        }
        [HttpGet]
        public async Task<IActionResult> EquipmentList()
        {

            return View();

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

        public async Task<IActionResult> List1()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> List1(DataSourceRequest command, EquipmentModel model)
        {
            var vessels = await _equipmentService.GetAllEquipment(model.SearchName, command.Page - 1, command.PageSize, true);

            var gridModel = new DataSourceResult {
                Data = vessels.ToList()
            };
            return Json(gridModel);

        }

        public async Task<IActionResult> Edit(string id)
        {
            
            List<Equipment> hopeList = new List<Equipment>();
            List<EquipmentModel> equipmentModels = new List<EquipmentModel>();
            var VesselName = HttpContext.Session.GetString("VesselName").ToString();
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

            return View(equipmentModels);
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
                    Frequency = item2.Frequency,
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
        //[HttpGet]
        //public async Task<IActionResult> Item(VesselForDisplay vesselForDisplay, string id)
        //{
        //    id = vesselForDisplay.VesselID;
        //    var vessel = await _vesselService.GetVesselById(id);
        //    vessel.Vessel_type = vesselForDisplay.Vessel_type;
        //    vessel.Vessel_name = vesselForDisplay.Vessel_name;

        //    vessel.IMO = vesselForDisplay.IMO;
        //    vessel.Class = vesselForDisplay.Class;
        //    vessel.Hull_no = vesselForDisplay.Hull_no;
        //    vessel.Shipyard = vesselForDisplay.Shipyard;
        //    vessel.Flag = vesselForDisplay.Flag;
        //    vessel.Main_Engine = vesselForDisplay.Main_Engine;
        //    vessel.Auxiliary_Engine = vesselForDisplay.Auxiliary_Engine;

        //    await _vesselService.UpdateVessel(vessel);
        //    return RedirectToAction("List");
        //}
    }
    }
