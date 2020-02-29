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
    public class MdmController : BaseAdminController
    {
        private readonly IMakerService _makerService;
        private readonly IMakerService1 _makerService1;
        private readonly IMakerViewModelService _makerViewModelService;
        private readonly IMakerViewModelService1 _makerViewModelService1;
        public MdmController(IMakerViewModelService _makerViewModelService, IMakerService _makerService, IMakerViewModelService1 _makerViewModelService1, IMakerService1 _makerService1)
        {
            this._makerViewModelService = _makerViewModelService;
            this._makerService = _makerService;
            this._makerViewModelService1 = _makerViewModelService1;
            this._makerService1 = _makerService1;
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
        public async Task<IActionResult> AddMaker()
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
