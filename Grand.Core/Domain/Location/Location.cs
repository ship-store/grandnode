using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.LocationEntity
{
    public partial class Location: BaseEntity
    {
        
        public string Locations{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
