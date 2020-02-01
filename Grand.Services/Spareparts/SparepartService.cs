using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.Sparepart;

namespace Grand.Services.Spareparts
{
    public  class SparepartService:ISparepartService
    {
        private readonly IRepository<Grand.Core.Domain.Sparepart.Sparepart> _sparepartRepository;
        public SparepartService(IRepository<Grand.Core.Domain.Sparepart.Sparepart> _sparepartRepository)
        {
            this._sparepartRepository = _sparepartRepository;
        }
       
        public virtual async Task InsertSparepart(Sparepart sparepart)
        {
            await _sparepartRepository.InsertAsync(sparepart);

        }


        async Task<IPagedList<Core.Domain.Sparepart.Sparepart>> ISparepartService.GetAllSpareparts(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _sparepartRepository.Table;



            return await PagedList<Grand.Core.Domain.Sparepart.Sparepart>.Create(query, pageIndex, pageSize);
        }



        public virtual Task<Grand.Core.Domain.Sparepart.Sparepart> GetSparepartById(string sparepartId)
        {
            return _sparepartRepository.GetByIdAsync(sparepartId);
        }
        public virtual async Task UpdateSparePart(Core.Domain.Sparepart.Sparepart sparepart)
        {
            await _sparepartRepository.UpdateAsync(sparepart);
        }
    }
}
