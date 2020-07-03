using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.MaintenanceTypeEntity
{
    public partial class MaintenanceType: BaseEntity
    {
        
        public string Maintenance_type{ get; set; }
        
    }
}
