using Grand.Framework.Mvc.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class CBMMappingModel : BaseGrandEntityModel
    {
        public string Cbm_Name { get; set; }
        public string equipmentComponent { get; set; }

    }
}
