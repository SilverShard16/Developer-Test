using Developer_Test.Services.Models;
using Developer_Test.Services.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer_Test.Services.Services
{
    public class VouchersService : IVouchersService
    {
        private readonly IJsonService _jsonService;

        public VouchersService(IJsonService jsonService)
        {
            _jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
        }

        public double ApplyDiscountToTotal(double subTotal, Voucher voucher)
        {
            var voucherIsPercentage = voucher.DiscountValue.Contains('%');

            float discount = voucherIsPercentage ?
                int.Parse(voucher.DiscountValue.Remove(voucher.DiscountValue.Length - 1)) :
                int.Parse(voucher.DiscountValue);

            if (discount >= subTotal && !voucherIsPercentage)
                discount = (float)subTotal;


            return voucherIsPercentage ?
                subTotal - (subTotal * (discount / 100)) :
                subTotal - discount;
        }

        public async Task<Voucher> GetVoucherByCodeAsync(string voucherCode)
        {
            try
            {
                var vouchers = await _jsonService.LoadVouchersAsync();

                var foundVouchers = vouchers.Where(x => x.DiscountCode == voucherCode.ToUpper()).OrderByDescending(y => y.ValidFrom);

                if (!foundVouchers.Any())
                    return null;

                var voucher = foundVouchers.FirstOrDefault();

                if (!IsVoucherValid(voucher))
                    return null;

                return voucher;
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<double> SubmitCouponAsync(double basketCost, string voucherCode)
        {
            var voucher = await GetVoucherByCodeAsync(voucherCode);

            if (voucher == null)
                return basketCost;

            return ApplyDiscountToTotal(basketCost, voucher);
        }

        public bool IsVoucherValid(Voucher voucher)
            => DateTime.Parse(voucher.ValidFrom) <= DateTime.UtcNow && DateTime.Parse(voucher.ValidTo) >= DateTime.UtcNow;

        
    }
}
