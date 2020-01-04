using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.Register;
using Grand.Services.register;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.register
{
    public class RegisterService : IRegisterService
    {


        private readonly IRepository<Register> _registerRepository;
        
        public RegisterService(IRepository<Register> registerRepository)
        {
            this._registerRepository = registerRepository;
        }

        //public RegisterService(IRepository<mLoginD> loginRepository)
        //{
        //    this._loginRepository = loginRepository;
        //}

        //public async Task FetchRegister(mLoginD login)
        //{
        //    await _loginRepository.InsertAsync(login);
        //}

        //public async Task FetchRegister(Register login)
        //{
        //    await _loginRepository.InsertAsync(login);
        ////}

        public virtual async Task InsertRegister(Register register)
        {
            await _registerRepository.InsertAsync(register);
        }

     

        public async Task<IList<Register>> PrepareLogin(Register model) 
        {
            var query = _registerRepository.Table;

            await Task.FromResult(0);


             
           
            return await PagedList<Register>.Create(query, 0, 15);
            


        }

        

     
    }
}
