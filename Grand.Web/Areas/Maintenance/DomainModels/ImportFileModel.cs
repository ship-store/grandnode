using Grand.Framework.Mvc.Models;

using System;
using System.Collections.Generic;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class ImportFileModel : BaseGrandEntityModel
    {
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string Status { get; set; }
        public int TotalCount { get; set; }
        public int ImportedCount { get; set; }
        public string CustomerName { get; set; }
    }

    public class ImportFileMapModel : BaseGrandEntityModel
    {
        public ImportFile ImportFile { get; set; }

        public Dictionary<string, string> PropertyMap { get; set; }
    }
}
