using Grand.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class ImportFile : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public int TotalCount { get; set; }
        public int ImportedCount { get; set; }
        public string CustomerId { get; set; }
    }
}