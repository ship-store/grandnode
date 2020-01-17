using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.UnplannedJobs
{
    public partial class UnplannedJob : BaseEntity
    {
        // public string ProductId { get; set; }

        public string EquipmentName { get; set; }
        public string JobOrder { get; set; }
        public string Title { get; set; }
        public string JobReportedDate { get; set; }
        public string ReportedBy { get; set; }
        public string Status { get; set; }
        public string Vessel { get; set; }

    }
}


