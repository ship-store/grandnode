﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.CbmEntity
{
    public partial class CBM: BaseEntity
    {
        
        public string Cbm_Name{ get; set; }
        public int DeleteStatus { get; set; }

    }
}
