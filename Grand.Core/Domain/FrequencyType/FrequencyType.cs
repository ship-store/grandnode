using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.FrequencyTypeEntity
{
    public partial class FrequencyType: BaseEntity
    {
        
        public string Frequency_type{ get; set; }
        
    }
}
