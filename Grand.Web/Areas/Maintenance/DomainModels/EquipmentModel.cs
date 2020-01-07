using Grand.Core;
using Grand.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class EquipmentModel: BaseGrandEntityModel
    {
        public string Sub1_number { get; set; }
        public string Sub1_description { get; set; }
        public string Sub2_number { get; set; }
        public string Sub2_description { get; set; }
        public string Sub3_number { get; set; }
        public string Sub3_description { get; set; }
        public string Sub4_number { get; set; }
        public string Sub4_description { get; set; }
        public string Sub5_number { get; set; }
        public string Sub5_description { get; set; }
        public string Safety_level { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Equipment_type     { get; set; }
        public string Drawing_no { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Equipment_Status { get; set; }
        public string Remark { get; set; }
        public string Vessel { get; set; }
    }
}
