using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Department
{
    public interface IDepartmentService
    {
         Task<IPagedList<Grand.Core.Domain.DepartmentEntity.Department>> GetAllDepartments(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.DepartmentEntity.Department>> GetAllDepartmentAsList();
           Task PrepareDepartmentModel(Grand.Core.Domain.DepartmentEntity.Department model1, object p, bool v);
       
        Task InsertDepartment(Core.Domain.DepartmentEntity.Department department);
        Task<Core.Domain.DepartmentEntity.Department> GetDepartmentById(string Id);
        Task UpdateDepartment(Core.Domain.DepartmentEntity.Department department);
    }
}
