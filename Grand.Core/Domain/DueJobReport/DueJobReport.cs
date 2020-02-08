using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.DueJobReport
{
    public class DueJobReport : BaseEntity
    {
        public string EquipmentCode { get; set; }
        public string JobType { get; set; }
        public string EquipmentName { get; set; }
        public string Vessel { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string CalFrequency { get; set; }
        public string FrequencyType { get; set; }
        public string Department { get; set; }
        //public string Priority { get; set; }
        //public string Rank { get; set; }
        public string AssignedTo { get; set; }
        public string LAST_DONE_DATE { get; set; }
        public string NEXT_DUE_DATE { get; set; }
        public string Job_Type { get; set; }
        public string Maintenance_Type { get; set; }
        public string JobCompletedDate { get; set; }

        public int JobStatus { get; set; }

        public string JobPlanStatus { get; set; }
        public int JobOrder { get; set; }
        public string Reading { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
    }
}
