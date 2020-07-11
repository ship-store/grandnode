using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.ReportedBy
{
    public class ReportedByService : IReportedByService
    {
        private readonly IRepository<Grand.Core.Domain.ReportedByEntity.ReportedBy> _reportedByRepository;
        
        public ReportedByService(IRepository<Grand.Core.Domain.ReportedByEntity.ReportedBy> _reportedByRepository)
        {
            this._reportedByRepository = _reportedByRepository;
        }
       
        async Task<IPagedList<Core.Domain.ReportedByEntity.ReportedBy>> IReportedByService.GetAllReportedBy(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _reportedByRepository.Table;
         
            return await PagedList< Grand.Core.Domain.ReportedByEntity.ReportedBy>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.ReportedByEntity.ReportedBy>> IReportedByService.GetAllReportedByAsList()
        {
            var query = _reportedByRepository.Table;


           

            return await PagedList<Grand.Core.Domain.ReportedByEntity.ReportedBy>.Create(query ,0,15);
        }

        Task IReportedByService.PrepareReportedByModel(Core.Domain.ReportedByEntity.ReportedBy model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertReportedBy(Core.Domain.ReportedByEntity.ReportedBy reportedBy)
        {


            await _reportedByRepository.InsertAsync(reportedBy);


        }
        public virtual async Task UpdateReportedBy(Core.Domain.ReportedByEntity.ReportedBy reportedBy)
        {
            await _reportedByRepository.UpdateAsync(reportedBy);
        }
        public virtual Task<Core.Domain.ReportedByEntity.ReportedBy> GetReportedByById(string reportedBy)
        {
            return _reportedByRepository.GetByIdAsync(reportedBy);
        }

    }
}
