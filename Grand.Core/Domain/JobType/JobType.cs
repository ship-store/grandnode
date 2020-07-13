using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.JobType
{
    public partial class JobType: BaseEntity
    {
        
        public string Job_type{ get; set; }
        public int DeleteStatus { get; set; }
    }
}
