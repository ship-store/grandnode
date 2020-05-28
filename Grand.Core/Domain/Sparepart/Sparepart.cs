using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.Sparepart
{
    public class Sparepart : BaseEntity
    {
        public string Vessel { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentCode { get; set; }
        public string SPAR_PARTS_DESCRIPTION { get; set; }
        public string PART_NUMBER { get; set; }
        public string DRAWING_NO { get; set; }
        public string SPECIFICATION { get; set; }
        public string POSITION_NUMBER { get; set; }
        
       

    }
    public class SparepartForDisplay : BaseEntity
    {
        public string Vessel { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentCode { get; set; }
        public string SPAR_PARTS_DESCRIPTION { get; set; }
        public string PART_NUMBER { get; set; }
        public string DRAWING_NO { get; set; }
        public string SPECIFICATION { get; set; }
        public string POSITION_NUMBER { get; set; }



    }
}
