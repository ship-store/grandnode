
using Grand.Web.Areas.Admin.Controllers;

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
using OfficeOpenXml;
using System.Data;

using Grand.Services.Jobplan;
using Grand.Core.Domain.Jobplan;
using Grand.Web.Areas.Maintenance.Services;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Core.Domain.Sparepart;
using Grand.Services.Spareparts;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class SparepartImportController : BaseAdminController
    {
        private readonly ISparepartImportManger _sparepartImportManger;
        private readonly IImportFileService _importFileService;
        private readonly ISparepartService _sparepartService;
        public SparepartImportController(ISparepartImportManger _sparepartImportManger, IImportFileService _importFileService,
            ISparepartService _sparepartService)
        {
            this._sparepartImportManger = _sparepartImportManger;
            this._importFileService = _importFileService;
            this._sparepartService = _sparepartService;
        }
        public IActionResult Index()
        {
            return View("List");
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
                        var equipmentImportModel = _sparepartImportManger.ImportFromXlsx(importexcelfile.OpenReadStream());


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


        public async Task<IActionResult> Map(string id)
        {
            await Task.FromResult(0);
            var importFile = await _importFileService.GetById(id);
            if (importFile.Status == "Pending")
            {
                dynamic allItems = JsonConvert.DeserializeObject(importFile.Content);



                foreach (var item in allItems)
                {
                    Sparepart sparepart = new Sparepart();
                    sparepart.Vessel = item["Vessel"];
                    sparepart.EquipmentName = item["EquipmentName"];
                    sparepart.EquipmentCode = item["EquipmentCode"];
                    sparepart.SPAR_PARTS_DESCRIPTION = item["SPAR_PARTS_DESCRIPTION"];
                    sparepart.PART_NUMBER = item["PART_NUMBER"];
                    sparepart.DRAWING_NO = item["DRAWING_NO"];
                    sparepart.SPECIFICATION = item["SPECIFICATION"];
                    sparepart.POSITION_NUMBER = item["POSITION_NUMBER"];
                    await _sparepartService.InsertSparepart(sparepart);
                }
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
                // Deserialize the input json string to an object
                input = Newtonsoft.Json.JsonConvert.DeserializeObject(input);

                // Json Object could either contain an array or an object or just values
                // For the field names, navigate to the root or the first element
                input = input.Root ?? input.First ?? input;

                if (input != null)
                {
                    // Get to the first element in the array
                    bool isArray = true;
                    while (isArray)
                    {
                        input = input.First ?? input;

                        if (input.GetType() == typeof(Newtonsoft.Json.Linq.JObject) ||
                        input.GetType() == typeof(Newtonsoft.Json.Linq.JValue) ||
                        input == null)
                            isArray = false;
                    }

                    // check if the object is of type JObject. 
                    // If yes, read the properties of that JObject
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
