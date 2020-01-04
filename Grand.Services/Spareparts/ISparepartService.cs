
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Spareparts
{
    public interface ISparepartService
    {
        Task InsertSparepart(Grand.Core.Domain.Sparepart.Sparepart sparepart);
    }
}
