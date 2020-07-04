using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.RankEntity
{
    public partial class Rank: BaseEntity
    {
        
        public string Ranks{ get; set; }
        
    }
}
