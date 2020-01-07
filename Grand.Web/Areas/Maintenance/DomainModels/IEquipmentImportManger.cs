using Grand.Core;
using Grand.Core.Domain.Catalog;
using Grand.Services.ExportImport.Help;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Grand.Web.Areas.Maintenance.DomainModels;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public interface IEquipmentImportManger
    {
        EquipmentImportModel ImportFromXlsx(Stream stream);

    }

    public class EquipmentImportManger : IEquipmentImportManger
    {
        public EquipmentImportModel ImportFromXlsx(Stream stream)
        {
            using (var xlPackage = new ExcelPackage(stream))
            {
                using (DataTable dataTable = GetDataTableFromExcel(xlPackage))
                {
                    return new EquipmentImportModel {
                        Content = DataTableToJson(dataTable),
                        TotalCount = dataTable.Rows.Count
                    };
                }
            }
        }

        DataTable GetDataTableFromExcel(ExcelPackage pck)
        {
            var ws = pck.Workbook.Worksheets.First();
            var tbl = new DataTable();
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                tbl.Columns.Add(firstRowCell.Text);
            }
            var startRow = 2;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                DataRow row = tbl.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Value;
                }
            }
            return tbl;
        }
        string DataTableToJson(DataTable table)
        {
            var jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }
    }
}