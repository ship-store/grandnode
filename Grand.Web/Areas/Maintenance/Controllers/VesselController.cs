using Grand.Core.Domain.Vessel;
using Grand.Framework.Kendoui;
using Grand.Services.Vendors;
using Grand.Services.Vessel;

using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Admin.Models.Vendors;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

            //var model = new VesselListModel();
            //  await Task.FromResult(0);          

            //var vessels = await _vesselService.GetAllVessels("Vishnu", 0, 500, true);


            //List<Vessel> vesselList = vessels.ToList();
            //var gridModel = new DataSourceResult {
            //    Data = vessels.ToList()

            //};

            //return View(vesselList);
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


        [HttpGet]
        public async Task<IActionResult> TestImage()
        {
            var model = await Task.FromResult<object>(null);

            return View();
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



        [HttpGet]
        public async Task<IActionResult> AddVessel()
        {
            var model = await Task.FromResult<object>(null);

            return View();
        }
        public IActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadFiles1(IList<IFormFile> files)
        {

            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = env.WebRootPath + $@"\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {

                    fs.Flush();
                }

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVesselDetails(VesselModel addNewVessel)
        {
            var fileName = Path.GetFileName(addNewVessel.file.FileName);
            string uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName);
            var Maintenance = Path.Combine(env.WebRootPath, "Maintenance", "Vessel");
            var filePath = Path.Combine(Maintenance, uniqueFileName);
            addNewVessel.file.CopyTo(new FileStream(filePath, FileMode.Create));
            await _vesselViewModelService.PrepareVesselModel(addNewVessel, "Vessel", true);
            return RedirectToAction("List", "Vessel");
        }




        public async Task<IActionResult> Edit(string id)
        {
            var vessel = await _vesselService.GetVesselById(id);
            // VesselModel model = new VesselModel() { Id = vessel.Id, Vessel_name = vessel.Vessel_name, Vessel_type = vessel.Vessel_type };
            VesselForDisplay display = new VesselForDisplay { VesselID = vessel.Id, Hull_no = vessel.Hull_no, Auxiliary_Engine = vessel.Auxiliary_Engine, Main_Engine = vessel.Main_Engine, Shipyard = vessel.Shipyard, Flag = vessel.Flag, IMO = vessel.IMO, Class = vessel.Class, Vessel_name = vessel.Vessel_name, Vessel_type = vessel.Vessel_type };

            if (vessel == null)
                //No product found with the specified id
                return RedirectToAction("List");

            return View(display);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(VesselForDisplay vesselForDisplay, string id)
        {
            //var fileName = Path.GetFileName(vesselForDisplay.file.FileName);
            //string uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName);
            //var Maintenance = Path.Combine(env.WebRootPath, "Maintenance", "Vessel");
            //var filePath = Path.Combine(Maintenance, uniqueFileName);
            //vesselForDisplay.file.CopyTo(new FileStream(filePath, FileMode.Create));
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
            //vessel.file = vesselForDisplay.file.FileName;
            await _vesselService.UpdateVessel(vessel);
            return RedirectToAction("List");
        }




    }
}
