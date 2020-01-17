using Grand.Core.Domain.Register;
using Grand.Services.register;
using Grand.Web.Areas.Admin.Controllers;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class RegisterController : BaseAdminController
    {
        private readonly IRegisterViewModelService _registerViewModelService;
       
        private readonly IRegisterService _registerService;
        public RegisterController(
            IRegisterViewModelService registerViewModelService,
            IRegisterService registerService
            )
        {
            this._registerViewModelService = registerViewModelService;
            this._registerService = registerService;
        }
        [HttpGet]
        public async Task<IActionResult> AddRegister()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }
        [HttpGet]
        public IActionResult Success()
        {

            //Session value getting

            ViewBag.EmailAddress = HttpContext.Session.GetString("email").ToString();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddRegisterlDetails(RegisterModel addNewRegister)
        {
            await _registerViewModelService.PrepareRegisterModel(addNewRegister, "Register", true);
            return RedirectToAction("LoginPage", "Register");
        }
        [HttpGet]
        public virtual async Task<IActionResult> LoginPage()
        {
            await Task.FromResult(0);
            return View();
        }
        [HttpGet]
        public virtual async Task<IActionResult> Login(RegisterModel model)
        {


            Register register = new Register() { Email=model.Email,Password=model.Password,Firstname=model.Firstname};
            var RegList= await _registerService.PrepareLogin(register);
            if (RegList.ToList().Find(x => x.Email == model.Email && x.Password == model.Password) != null)
            {

                //Sesion
                HttpContext.Session.SetString("email", model.Email);
                return RedirectToAction("Success");
            }
        

            return RedirectToAction("LoginPage", "Register");
        }

        [HttpGet]
        public virtual async Task<IActionResult> Logout(RegisterModel model)
        {
            HttpContext.Session.Clear();
            return this.RedirectToAction("LoginPage", "Register", new { area = "Maintenance" });
        }
    }
    
}

