using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain
{
   public class JobMaster:BaseEntity
    {
        public string JobCode { get; set; }
        public string JobType{ get; set; }
        public string JobTitle { get; set; }
        public string JobDescription{ get; set; }
    }
}
