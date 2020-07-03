using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.FrequencyEntity
{
    public partial class Frequency: BaseEntity
    {
        
        public string Frequencies{ get; set; }
        
    }
}
