using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.EquipmentTypeEntity
{
    public partial class EquipmentType: BaseEntity
    {
        
        public string Equipment_type{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
