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
using Grand.Services.Jobplan;
using Grand.Core.Domain.Jobplan;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class RegisterController : BaseAdminController
    {
        private readonly IRegisterViewModelService _registerViewModelService;
        private readonly IJobplanService _jobplanService;
        private readonly IRegisterService _registerService;
        public RegisterController(
            IRegisterViewModelService registerViewModelService,
            IRegisterService registerService,
            IJobplanService _jobplanService
            )
        {
            this._registerViewModelService = registerViewModelService;
            this._registerService = registerService;
            this._jobplanService = _jobplanService;
        }
        [HttpGet]
        public async Task<IActionResult> AddRegister()
        {
            var model = await Task.FromResult<object>(null);
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult >Success()
        {
            //Session value gettings
            try
            {
                ViewBag.EmailAddress = HttpContext.Session.GetString("email").ToString();


            }
            catch (System.Exception)
            {
                return RedirectToAction("LoginPage", "Register");
            }

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
            try
            {
                Register register = new Register() { Email = model.Email, Password = model.Password, Firstname = model.Firstname };
                var RegList = await _registerService.PrepareLogin(register);
                if (RegList.ToList().Find(x => x.Email.ToLower() == model.Email.ToLower() && x.Password ==model.Password) != null)
                {
                    //Sesion
                    HttpContext.Session.SetString("email", model.Email);
                    return RedirectToAction("Success");
                }

                return RedirectToAction("LoginPage", "Register");
            }
            catch (System.Exception)
            {
                return RedirectToAction("LoginPage", "Register");
            }            
        }

        [HttpGet]
        public virtual async Task<IActionResult> Logout(RegisterModel model)
        {
            HttpContext.Session.Clear();
            return this.RedirectToAction("LoginPage", "Register", new { area = "Maintenance" });
        }
    }
    
}

