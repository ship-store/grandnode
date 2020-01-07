using Grand.Core.Data;
using Grand.Core.Domain.Catalog;

using Grand.Services.Catalog;
using Grand.Web.Areas.Maintenance.DomainModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{

    public class ImportFileService : IImportFileService
    {
        private readonly IRepository<ImportFile> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        public ImportFileService(IRepository<ImportFile> repository, IRepository<Category> categoryRepository, IRepository<SpecificationAttribute> specificationAttributeRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _specificationAttributeRepository = specificationAttributeRepository;
        }
        public virtual async Task Delete(ImportFile record)
        {
            if (record == null)
                throw new NullReferenceException("Null record object");

            await _repository.DeleteAsync(record);
        }

        public virtual async Task<IList<ImportFile>> GetAll()
        {
            return await _repository.Table.ToListAsync();
        }
        public virtual async Task<ImportFile> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new NullReferenceException("Id is null");

            return await _repository.GetByIdAsync(id);
        }
        public virtual async Task<ImportFile> Insert(ImportFile record)
        {
            if (record == null)
                throw new NullReferenceException("Null record object");



            return await _repository.InsertAsync(record);
        }
        public virtual async Task<ImportFile> Update(ImportFile record)
        {
            if (record == null)
                throw new NullReferenceException("Null record object");

            return await _repository.UpdateAsync(record);
        }
        public virtual async Task<IList<Category>> GetAllCategory()
        {
            return await _categoryRepository.Table.ToListAsync();
        }
        public virtual async Task<Category> GetCategoryByName(string name)
        {
            return await _categoryRepository.Collection.Find(f => f.Name == name).FirstOrDefaultAsync();
        }
        public virtual async Task<SpecificationAttribute> GetSpecificationAttributeByName(string name)
        {
            return await _specificationAttributeRepository.Collection.Find(f => f.Name == name).FirstOrDefaultAsync();
        }
    }
}
