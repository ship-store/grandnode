
using Grand.Core.Domain.Register;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Grand.Services.register
{
    public interface IRegisterService
    {
        Task InsertRegister(Register register);
       
        Task<IList<Register>> PrepareLogin(Register model );
    }
}
