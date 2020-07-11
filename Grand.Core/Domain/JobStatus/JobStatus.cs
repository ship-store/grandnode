using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.JobStatusEntity
{
    public partial class JobStatus: BaseEntity
    {
        
        public string Status{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
