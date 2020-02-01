
using Grand.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Spareparts
{
    public interface ISparepartService
    {
        Task InsertSparepart(Grand.Core.Domain.Sparepart.Sparepart sparepart);
       
         Task<IPagedList<Grand.Core.Domain.Sparepart.Sparepart>> GetAllSpareparts(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        
        Task<Grand.Core.Domain.Sparepart.Sparepart> GetSparepartById(string Id);
        Task UpdateSparePart(Grand.Core.Domain.Sparepart.Sparepart sparepart);

    }
}
