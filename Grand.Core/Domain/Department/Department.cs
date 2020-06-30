using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Core.Domain.DepartmentEntity
{
    public partial class Department: BaseEntity
    {
        
        public string Departments{ get; set; }
        
    }
}
