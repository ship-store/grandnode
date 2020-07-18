using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.EquipmentStatusEntity
{
    public partial class EquipmentStatus: BaseEntity
    {
        
        public string Status{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
