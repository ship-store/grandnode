using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.CbmEntity
{
    public partial class CBMMapping : BaseEntity
    {
        
        public string Cbm_Name{ get; set; }
        public string equipmentComponent { get; set; }// EquipmentType
        public string jobCode { get; set; }
        
    }
}

