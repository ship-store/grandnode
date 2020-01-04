using Grand.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class JobplanModel : BaseEntity
    {
        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public string Vessel { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Frequency { get; set; }
        public string FrequencyType { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string Rank { get; set; }
        public string AssignedTo { get; set; }
        public string LAST_DONE_DATE { get; set; }
        public string NEXT_DUE_DATE { get; set; }
        public string Job_Type { get; set; }
        public string Maintenance_Type { get; set; }
    }
    public class JobplanDisplayModel : BaseEntity
    {
        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public string Vessel { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Frequency { get; set; }
        public string FrequencyType { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string Rank { get; set; }
        public string AssignedTo { get; set; }
        public string LAST_DONE_DATE { get; set; }
        public string NEXT_DUE_DATE { get; set; }
        public string Job_Type { get; set; }
        public string Maintenance_Type { get; set; }
    }
}
