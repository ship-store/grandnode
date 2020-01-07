
using Grand.Core.Domain.Register;
using Grand.Services.register;
using Grand.Web.Areas.Admin.Extensions;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public class RegisterViewModelService : IRegisterViewModelService
    {

        private readonly IRegisterService _registerService;
        private readonly IRegisterService _loginService;
        public RegisterViewModelService(IRegisterService registerService)
        {
            this._registerService = registerService;
            this._loginService = registerService;
        }
        public virtual async Task PrepareRegisterModel(RegisterModel model1, object p, bool v)
        {
            try
            {

                var register = new Register();
                register.Firstname = model1.Firstname;
                register.Secondname = model1.Secondname;
                register.Email = model1.Email;
                register.Password = model1.Password;
                register.ConfirmPassword = model1.ConfirmPassword;

                await _registerService.InsertRegister(register);

            }
            catch (Exception ex)
            {
                
            }
        }

        public Task PrepareRegisterModel(RegisterModel model, RegisterModel Register, bool setPredefinedvalues, bool excludeProperties)
        {
            throw new NotImplementedException();
        }

        public Task PrepareLogin(RegisterModel model2, string v, bool v1)
        {
            throw new NotImplementedException();
        }
    }
}
