using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Maker;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class DepartmentViewModelService : IDepartmentViewModelService
    {
        private readonly IDepartmentService _departmentService;
        private readonly IRepository<Department> _DepartmentRepository;

        public DepartmentViewModelService(IDepartmentService _departmentService,
            IRepository<Department> _DepartmentRepository)
        {
            this._departmentService = _departmentService;
            this._DepartmentRepository = _DepartmentRepository;

        }
        Task<IPagedList<Department>> IDepartmentViewModelService.GetAllDepartments(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<Department>> IDepartmentViewModelService.GetAllDepartmentAsList(string id)
        {
            await Task.FromResult(0);

            var query = _DepartmentRepository.Table;

           var result=await PagedList<Department>.Create(query, 0,15);

            return result;
        }
        async Task IDepartmentViewModelService.PrepareDepartmentModel(DepartmentModel addNewDepartment, object p, bool v)
        {
            try
            {

                var department = new Department();

                department.Departments = addNewDepartment.Departments;
               
                await  _departmentService.InsertDepartment(department);
            }
            catch (Exception ex)
            {
                var department = new Department();

                department.Departments = addNewDepartment.Departments;

                await _departmentService.InsertDepartment(department);

            }
        }

        //public Task<IPagedList<Department>> GetAllDepartments(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
