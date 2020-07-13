using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.SafetyLevelEntity
{
    public partial class SafetyLevel: BaseEntity
    {
        
        public string Safety_level{ get; set; }
        public int DeleteStatus { get; set; }


    }
}
