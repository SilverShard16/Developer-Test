using Developer_Test.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Developer_Test.Services.Services.Interfaces
{
    public interface IJsonService
    {
        Task<List<Voucher>> LoadVouchersAsync();
    }
}
