using Grand.Framework.Mvc.ModelBinding;
using Grand.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class UnplannedJobModel : BaseGrandEntityModel
    {
        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.equipment_name")]
        public string EquipmentName { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.job_order")]
        public string JobOrder { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.title")]
        public string Title { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.jobreported_date")]
        public string JobReportedDate { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.reported_by")]
        public string ReportedBy { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.status")]
        public string Status { get; set; }
    }
    public class UnplannedJobDisplayModel : BaseGrandEntityModel
    {
        public string UnplannedJobID { get; set; }
        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.equipment_name")]
        public string EquipmentName { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.job_order")]
        public string JobOrder { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.title")]
        public string Title { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.jobreported_date")]
        public string JobReportedDate { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.reported_by")]
        public string ReportedBy { get; set; }

        [GrandResourceDisplayName("maintenance.unplannedjobs.fields.status")]
        public string Status { get; set; }
    }
}
