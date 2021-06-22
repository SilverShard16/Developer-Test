using Developer_Test.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer_Test.Services.Services.Interfaces
{
    public interface IVouchersService
    {
        Task<Voucher> GetVoucherByCodeAsync(string voucherCode);
        bool IsVoucherValid(Voucher voucher);
        double ApplyDiscountToTotal(double subTotal, Voucher voucher);
        Task<double> SubmitCouponAsync(double basketCost, string voucherCode);
    }
}
