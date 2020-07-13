using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Grand.Core.Domain.DepartmentEntity.Department> _departmentRepository;
        
        public DepartmentService(IRepository<Grand.Core.Domain.DepartmentEntity.Department> _departmentRepository)
        {
            this._departmentRepository = _departmentRepository;
        }
       
        async Task<IPagedList<Core.Domain.DepartmentEntity.Department>> IDepartmentService.GetAllDepartments(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _departmentRepository.Table;
         
            return await PagedList< Grand.Core.Domain.DepartmentEntity.Department>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.DepartmentEntity.Department>> IDepartmentService.GetAllDepartmentAsList()
        {
            var query = _departmentRepository.Table;


           

            return await PagedList<Grand.Core.Domain.DepartmentEntity.Department>.Create(query ,0,15);
        }

        Task IDepartmentService.PrepareDepartmentModel(Core.Domain.DepartmentEntity.Department model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertDepartment(Core.Domain.DepartmentEntity.Department department)
        {


            await _departmentRepository.InsertAsync(department);


        }
        public virtual Task<Core.Domain.DepartmentEntity.Department> GetDepartmentById(string departmentId)
        {
            return _departmentRepository.GetByIdAsync(departmentId);
        }
        public virtual async Task UpdateDepartment(Core.Domain.DepartmentEntity.Department department)
        {
            await _departmentRepository.UpdateAsync(department);
        }

    }
}
