using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Jobplan
{
    public interface IJobplanService
    {
        Task InsertJobplan(Grand.Core.Domain.Jobplan.Jobplan jobplan);
    }
}
