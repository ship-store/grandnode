using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Grand.Services.Equipments;
using Grand.Services.Vessel;
using OfficeOpenXml;
using System.Data;
using Grand.Web.Areas.Maintenance.Services;
using Grand.Core.Domain.Equipment;
using Grand.Core.Domain.Vessel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Grand.Framework.Kendoui;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class ImportEquipmentController : BaseAdminController
    {
        private readonly IVesselService _vesselService;
        private readonly IHostingEnvironment env;
        private readonly EquipmentImportManger _equipmentImportManger;
        private readonly IImportFileService _importFileService;
        private readonly IEquipmentService _equipmentService;
        private readonly IEquipmentViewModelService _equipmentViewModelService;
        public ImportEquipmentController(EquipmentImportManger _equipmentImportManger, IImportFileService _importFileService,
            IEquipmentService _equipmentService, IVesselService _vesselService, IHostingEnvironment env, IEquipmentViewModelService _equipmentViewModelService)
        {
            this._equipmentImportManger = _equipmentImportManger;
            this._importFileService = _importFileService;
            this._equipmentService = _equipmentService;
            this._vesselService = _vesselService;
            this.env = env;
            this._equipmentViewModelService = _equipmentViewModelService;
        }
        public IActionResult Index()
        {
            return View("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new VesselListModel();
            var vessels = await _vesselService.GetAllVessels("null", 0, 500, true);
            List<Vessel> vesselList = vessels.ToList();
            string spreadsheetPath = "vesselList.xls";
            System.IO.File.Delete(spreadsheetPath);
            FileInfo spreadsheetInfo = new FileInfo(spreadsheetPath);
            ExcelPackage pck = new ExcelPackage(spreadsheetInfo);
            var vesselsWorksheet = pck.Workbook.Worksheets.Add("vesselList");
            vesselsWorksheet.Cells["A1"].Value = "Vessel_name";

            vesselsWorksheet.Cells["A1:Ii 1"].Style.Font.Bold = true;

            int currentRow = 2;
            foreach (var activity in vesselList)
            {
                vesselsWorksheet.Cells["A" + currentRow.ToString()].Value = activity.Vessel_name;

                currentRow++;
            }
            byte[] data = pck.GetAsByteArray();
            ViewBag.path = env.WebRootPath;
            string swwwRootPath = env.WebRootPath + "/" + "vesselList.xls";
            System.IO.File.WriteAllBytes(swwwRootPath, data);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel()// add to mogodb
        {
            try
            {
                var files = Request.Form.Files;

                if (files.Any())
                {
                    var importexcelfile = files.First();
                    if (importexcelfile != null && importexcelfile.Length > 0)
                    {
                        DateTime now = DateTime.Now;
                        var equipmentImportModel = _equipmentImportManger.ImportFromXlsx(importexcelfile.OpenReadStream());
                        var importFile = await _importFileService.Insert(new Grand.Web.Areas.Maintenance.DomainModels.ImportFile {
                            Name = importexcelfile.FileName,
                            CreatedOnUtc = now,
                            Content = equipmentImportModel.Content,
                            Status = "Pending",
                            TotalCount = equipmentImportModel.TotalCount,
                    });
                        SuccessNotification("File uploaded successfully.");
                        return RedirectToAction("Map", new { id = importFile.Id });
                    }
                    else
                    {
                        return RedirectToAction("List");
                    }
                }
                SuccessNotification("Plesae select a file.");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }


        // list
      //  public IActionResult Index() => RedirectToAction("List");
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

        public async Task<IActionResult> Map(string id)
        {
            await Task.FromResult(0);
            var importFile = await _importFileService.GetById(id);
            if (importFile.Status == "Pending")
            {
                dynamic allItems = JsonConvert.DeserializeObject(importFile.Content);
                foreach (var item in allItems)
                {
                    Equipment newEquipment = new Equipment();
                    newEquipment.Sub1_number = item["Sub1_number"];
                    newEquipment.Sub1_description = item["Sub1_description"];
                    newEquipment.Sub2_number = item["Sub2_number"];
                    newEquipment.Sub2_description = item["Sub2_description"];
                    newEquipment.Sub3_number = item["Sub3_number"];
                    newEquipment.Sub3_description = item["Sub3_description"];
                    newEquipment.Sub4_number = item["Sub4_number"];
                    newEquipment.Sub4_description = item["Sub4_description"];
                    newEquipment.Sub5_number = item["Sub5_number"];
                    newEquipment.Sub5_description = item["Sub5_description"];
                    newEquipment.Safety_level = item["Safety_level"];
                    newEquipment.Maker = item["Maker"];
                    newEquipment.Model = item["Model"];
                    newEquipment.Drawing_no = item["Drawing_no"];
                    newEquipment.Department = item["Department"];
                    newEquipment.Location = item["Location"];
                    newEquipment.Equipment_Status = item["Equipment_Status"];
                    newEquipment.Remark = item["Remark"];
                    newEquipment.Vessel = item["Vessel"];
                    newEquipment.Type = item["Type"];
                    // write Service

                    await _equipmentService.InsertEquipment(newEquipment);
                }
                //return Content("Already imported, Contact Admin");
            }
            var properties = GetFieldNames(importFile.Content);
            var propertyMap = new Dictionary<string, string>();
            var importFileMapModel = new ImportFileMapModel() {
                ImportFile = importFile,
                PropertyMap = propertyMap
            };

            return View("View", importFileMapModel);
        }

        public static List<string> GetFieldNames(dynamic input)
        {
            var fieldNames = new List<string>();

            try
            {
                input = Newtonsoft.Json.JsonConvert.DeserializeObject(input);
                input = input.Root ?? input.First ?? input;

                if (input != null)
                {
                    bool isArray = true;
                    while (isArray)
                    {
                        input = input.First ?? input;

                        if (input.GetType() == typeof(Newtonsoft.Json.Linq.JObject) ||
                        input.GetType() == typeof(Newtonsoft.Json.Linq.JValue) ||
                        input == null)
                            isArray = false;
                    }

                    if (input.GetType() == typeof(Newtonsoft.Json.Linq.JObject))
                    {
                        // Create JObject from object
                        Newtonsoft.Json.Linq.JObject inputJson =
                            Newtonsoft.Json.Linq.JObject.FromObject(input);
                        // Read Properties
                        var properties = inputJson.Properties();
                        // Loop through all the properties of that JObject
                        foreach (var property in properties)
                        {
                            // Check if there are any sub-fields (nested)
                            // i.e. the value of any field is another JObject or another JArray
                            if (property.Value.GetType() == typeof(Newtonsoft.Json.Linq.JObject) ||
                            property.Value.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
                            {
                                // If yes, enter the recursive loop to extract sub-field names
                                var subFields = GetFieldNames(property.Value.ToString());

                                if (subFields != null && subFields.Count() > 0)
                                {
                                    // join sub-field names with field name 
                                    //(e.g. Field1.SubField1, Field1.SubField2, etc.)
                                    fieldNames.AddRange(
                                        subFields
                                        .Select(n =>
                                        string.IsNullOrEmpty(n) ? property.Name :
                                     string.Format("{0}.{1}", property.Name, n)));
                                }
                            }
                            else
                            {
                                // If there are no sub-fields, the property name is the field name
                                fieldNames.Add(property.Name);
                            }
                        }
                    }
                    else
                        if (input.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
                    {
                        // for direct values, there is no field name
                        fieldNames.Add(string.Empty);
                    }
                }
            }
            catch
            {
                throw;
            }

            return fieldNames;
        }

        public async Task<ActionResult> DownloadExcelResult(string id)
        {
            var importFile = await _importFileService.GetById(id);
            var dataTable = GetDataTableFromJsonString(importFile.Content);
            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Data");
                ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        public DataTable GetDataTableFromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
