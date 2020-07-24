using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.CriticalEntity
{
    public partial class Critical: BaseEntity
    {
        
        public string Criticals{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
