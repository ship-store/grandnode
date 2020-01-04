using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    }
}
