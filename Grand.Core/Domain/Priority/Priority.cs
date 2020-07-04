using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.PriorityEntity
{
    public partial class Priority: BaseEntity
    {
        
        public string Priorities{ get; set; }
        
    }
}
