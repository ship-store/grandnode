using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IRegisterViewModelService
    {
        Task PrepareRegisterModel(RegisterModel model, RegisterModel Register, bool setPredefinedvalues, bool excludeProperties);
        Task PrepareRegisterModel(RegisterModel model1, object p, bool v);
        Task PrepareLogin(RegisterModel model2, string v, bool v1);
    }
}
