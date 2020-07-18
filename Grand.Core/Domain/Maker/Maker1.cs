using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.MakerEntity
{
    public partial class Maker1: BaseEntity
    {


       // public string MakerId { get; set; }
        public string Maker { get; set; }
        public string Model{ get; set; }
        public string Remark { get; set; }
        public int DeleteStatus { get; set; }





    }
}
