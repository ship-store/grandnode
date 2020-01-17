using Grand.Framework.Mvc.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class BreakdownJobModel : BaseGrandEntityModel
    {
        public string EquipmentName { get; set; }
        public string JobOrder { get; set; }
        public string Title { get; set; }
        public string JobReportedDate { get; set; }
        public string ReportedBy { get; set; }
        public string Status { get; set; }
        public string Vessel { get; set; }
    }
    public class BreakdownJobDisplayModel : BaseGrandEntityModel
    {
        public string BreakdownJobID { get; set; }
        public string EquipmentName { get; set; }
        public string JobOrder { get; set; }
        public string Title { get; set; }
        public string JobReportedDate { get; set; }
        public string ReportedBy { get; set; }
        public string Status { get; set; }
        public string Vessel { get; set; }
    }
}
