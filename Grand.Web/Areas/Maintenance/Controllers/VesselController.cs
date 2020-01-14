﻿using Grand.Core.Domain.Vessel;
using Grand.Framework.Kendoui;
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
namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class VesselController : BaseAdminController
    {
        private readonly IVesselService _vesselService;
        private readonly IHostingEnvironment env;
        private readonly IVesselViewModelService _vesselViewModelService;
        public VesselController(IVesselViewModelService _vesselViewModelService, IVesselService _vesselService, IHostingEnvironment env)
        {
            this._vesselViewModelService = _vesselViewModelService;
            this._vesselService = _vesselService;
            this.env = env;
        }

        // list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> List()
        {
            await Task.FromResult(0);
           return View();
        }


        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command, VesselListModel model)
        {
            var vessels = await _vesselService.GetAllVessels(model.SearchName, command.Page - 1, command.PageSize, true);
            var gridModel = new DataSourceResult {
                Data = vessels.Where(x=>x.ActiveStatus==1).ToList()

            };
            return Json(gridModel);
        }
        [HttpGet]
        public async Task<IActionResult> AddVessel()
        {
            var model = await Task.FromResult<object>(null);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVesselDetails(VesselModel newVessel)
        {
            await _vesselViewModelService.PrepareVesselModel(newVessel);
            return RedirectToAction("List", "Vessel");
            
        }
        public async Task<IActionResult> Edit(string id)
        {
            var vessel = await _vesselService.GetVesselById(id);
            VesselForDisplay display = new VesselForDisplay { VesselID = vessel.Id,Hull_no = vessel.Hull_no,Auxiliary_Engine=vessel.Auxiliary_Engine, Main_Engine = vessel.Main_Engine, Shipyard = vessel.Shipyard, Flag = vessel.Flag, IMO = vessel.IMO, Class = vessel.Class, Vessel_name = vessel.Vessel_name, Vessel_type = vessel.Vessel_type };

            if (vessel == null)
                return RedirectToAction("List");

            return View(display);
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
    }
}
