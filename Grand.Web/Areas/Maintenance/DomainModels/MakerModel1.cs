using Grand.Framework.Mvc.ModelBinding;
using Grand.Framework.Mvc.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class MakerModel1 : MakerModel
    {
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Remark { get; set; }

        [GrandResourceDisplayName("Maintenance.Maker.List.SearchName")]
        public string SearchName { get; set; }
    }
}
