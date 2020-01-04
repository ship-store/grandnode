using Grand.Core.Domain.MakerEntity;
using Grand.Framework.Kendoui;


using Grand.Services.Maker;

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
    public class MakerController : BaseAdminController
    {
        private readonly IMakerService _makerService;
        private readonly IMakerService1 _makerService1;
        private readonly IMakerViewModelService _makerViewModelService;
        private readonly IMakerViewModelService1 _makerViewModelService1;
        public MakerController(IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1)
        {

            this._makerViewModelService = _makerViewModelService;
            this._makerService = _makerService;
            this._makerViewModelService1 = _makerViewModelService1;
            this._makerService1 = _makerService1;

        }

        // list
        public IActionResult Index() => RedirectToAction("List");
        public async Task<IActionResult> List()
        {

            var model = new MakerListModel();
            //   await Task.FromResult(0);          

            var makers = await _makerService.GetAllMakers("Vishnu", 0, 500, true);


            List<Maker> makerList = makers.ToList();
            var gridModel = new DataSourceResult {
                Data = makers.ToList()

            };

            return View(makerList);
            // return View();

        }


        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command, MakerListModel model)
        {
            var makers = await _makerService.GetAllMakers(model.SearchName, command.Page - 1, command.PageSize, true);

            var gridModel = new DataSourceResult {
                Data = makers.ToList()

            };
            return Json(gridModel);
        }


        //[HttpGet]
        //public async Task<IActionResult> List()
        //{

        //    // Vessel obj = new Vessel() { Id = "1212", Hull_no = "1213", Vessel_name = "Vishnu" };                     

        //    var vessels = await _vesselService.GetAllVesselAsList();
        //var gridModel = new DataSourceResult {
        //    Data = vessels.ToList()

        //};
        //    return Json(gridModel);


        //}




        [HttpGet]
        public async Task<IActionResult> AddMaker()
        {
            var model = await Task.FromResult<object>(null);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddMakerDetails(MakerModel addNewMaker)
        {
            await _makerViewModelService.PrepareMakerModel(addNewMaker, "Vishnu", true);
            return RedirectToAction("AddMaker", "Maker");
        }


        [HttpGet]
        public async Task<IActionResult> AddMakerDetails1(MakerModel1 addNewMaker1)
        {
            await _makerViewModelService1.PrepareMakerModel(addNewMaker1, "Vishnu", true);
            return RedirectToAction("List", "Maker");
        }



    }
}
