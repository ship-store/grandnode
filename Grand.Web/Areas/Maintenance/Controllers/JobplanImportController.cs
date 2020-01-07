﻿using Grand.Web.Areas.Admin.Controllers;
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
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Services;
using Grand.Core.Domain.Sparepart;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class JobplanImportController : BaseAdminController
    {
        private readonly IJobplanImportManger _jobplanImportManger;
        private readonly IImportFileService _importFileService;
        private readonly IJobplanService _jobplanService;
        public JobplanImportController(IJobplanImportManger _jobplanImportManger, IImportFileService _importFileService,
            IJobplanService _jobplanService)
        {
            this._jobplanImportManger = _jobplanImportManger;
            this._importFileService = _importFileService;
            this._jobplanService = _jobplanService;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Maintenance/Views/JobplanImport/List.cshtml");
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
                        var equipmentImportModel = _jobplanImportManger.ImportFromXlsx(importexcelfile.OpenReadStream());


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
                    Jobplan job = new Jobplan();
                    job.EquipmentCode = item["EquipmentCode"];
                    job.EquipmentName = item["EquipmentName"];
                    job.Vessel = item["Vessel"];
                    job.JobTitle= item["JobTitle"];
                    job.JobDescription = item["JobDescription"];
                    job.Frequency = item["Frequency"];
                    job.FrequencyType = item["FrequencyType"];
                    job.Department = item["Department"];
                    job.Priority = item["Priority"];
                    job.Rank = item["Rank"];
                    job.AssignedTo = item["AssignedTo"];
                    job.LAST_DONE_DATE = item["LAST_DONE_DATE"];
                    job.NEXT_DUE_DATE = item["NEXT_DUE_DATE"];
                    job.Job_Type = item["Job_Type"];
                    job.Maintenance_Type = item["Maintenance_Type"];
                    await _jobplanService.InsertJobplan(job);
                }
            }

            var properties = GetFieldNames(importFile.Content);
            var propertyMap = new Dictionary<string, string>();
           
            var importFileMapModel = new ImportFileMapModel() {
                ImportFile = importFile,
                PropertyMap = propertyMap
            };

            return View("~/Areas/Maintenance/Views/JobplanImport/View.cshtml", importFileMapModel);
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
