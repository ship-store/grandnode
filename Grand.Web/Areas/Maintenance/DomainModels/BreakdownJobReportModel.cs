using Grand.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class BreakdownJobReportModel : BaseGrandEntityModel
    {
        public string EquipmentName { get; set; }
        public int JobOrder { get; set; }
        public string Title { get; set; }
        public string JobReportedDate { get; set; }
        public string ReportedBy { get; set; }
        public string Status { get; set; }
        public string Vessel { get; set; }
        public string DeleteStatus { get; set; }
        public string JobCompletedDate { get; set; }
        public string Remark { get; set; }
    }
}
