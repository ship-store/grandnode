using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.Jobplan
{
    public class Jobplan : BaseEntity
    {
        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public string Vessel { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string CalFrequency { get; set; }
        public string RunFrequency { get; set; }
        public string FrequencyType { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string Rank { get; set; }
        public string AssignedTo { get; set; }
        public string LAST_DONE_DATE { get; set; }
        public string NEXT_DUE_DATE { get; set; }
        public string Job_Type { get; set; }
        public string Maintenance_Type { get; set; }
        public int JobStatus { get; set; }
        public string JobPlanStatus { get; set; }
        public int JobOrder { get; set; }
        public int InitialReading { get; set; }
        public int PreviousReading { get; set; }
        public int LastReading { get; set; }
        public int PlanHorizon { get; set; }
        public string JobCompletedDate { get; set; }
        public string Reading { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public int DueRhs { get; set; }
        public string UniversalJobCode { get; set; }

    }


    public class JobPlanForDisplay : BaseEntity
    {

        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public string Vessel { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string CalFrequency { get; set; }
        public string RunFrequency { get; set; }
        public string FrequencyType { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string Rank { get; set; }
        public string AssignedTo { get; set; }
        public DateTime LAST_DONE_DATE { get; set; }
        public DateTime NEXT_DUE_DATE { get; set; }
        public string Job_Type { get; set; }
        public string Maintenance_Type { get; set; }
        public int JobStatus { get; set; }
        public int JobOrder { get; set; }
        public int PreviousReading { get; set; }
        public int LastReading { get; set; }
        public int PlanHorizon { get; set; }
        public string Reading { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public int DueRhs { get; set; }
        public string UniversalJobCode { get; set; }
        
        public string Cbm { get; set; }
        public List<string> CbmParameterList{ get; set; } = new List<string>();

    }
}
