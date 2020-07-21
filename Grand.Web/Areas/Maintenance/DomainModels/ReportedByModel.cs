using Grand.Framework.Mvc.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class ReportedByModel : BaseGrandEntityModel
    {
        public string Reported_By { get; set; }
        public int DeleteStatus { get; set; }

    }
}
