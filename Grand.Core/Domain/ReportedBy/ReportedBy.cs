using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.ReportedByEntity
{
    public partial class ReportedBy: BaseEntity
    {
        
        public string Reported_By{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
