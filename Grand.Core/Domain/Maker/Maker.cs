using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.MakerEntity
{
    public partial class Maker: BaseEntity
    {
        
        public string Name{ get; set; }
        public string Code { get; set; }
        public string Country { get; set; }

    }
}
