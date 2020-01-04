using Grand.Core.Domain.Catalog;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public interface IImportFileService
    {
        Task<ImportFile> GetById(string id);
        Task<IList<ImportFile>> GetAll();
        Task<ImportFile> Insert(ImportFile record);
        Task<ImportFile> Update(ImportFile record);
        Task Delete(ImportFile record);
        Task<IList<Category>> GetAllCategory();
        Task<Category> GetCategoryByName(string name);
        Task<SpecificationAttribute> GetSpecificationAttributeByName(string name);

    }
}