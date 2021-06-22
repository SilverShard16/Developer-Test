using System;
using Xunit;
using Moq;
using Developer_Test.Services.Services.Interfaces;
using Developer_Test.WebApp.Controllers;
using System.Threading.Tasks;
using Developer_Test.Services.Models;
using Developer_Test.Services.Services;

namespace Developer_Test.Tests
{
    public class VoucherTests
    {
        [Fact]
        public async Task GetVoucher_WithIncorrectName_NoVouchersReturned()
        {
            //Setup
            var jsonService = Setup();
            var vouchersService = new VouchersService(jsonService.Object);

            //Action
            var result = await vouchersService.SubmitCouponAsync(3.60, "thiswontwork");

            //Assert
            Assert.Equal(3.60, Math.Round(result, 2));
        }

        [Fact]
        public async Task GetVoucher_WithCorrectName_BeforeStartDate_NoVouchersReturned_ReturnsSameValueInput()
        {
            //Setup
            var jsonService = Setup();
            var vouchersService = new VouchersService(jsonService.Object);

            //Action
            var result = await vouchersService.SubmitCouponAsync(3.60, "FAKEVOUCHER3");

            //Assert
            Assert.Equal(3.60, Math.Round(result, 2));
        }

        [Fact]
        public async Task GetVoucher_WithCorrectName_AfterExpiryDate_NoVouchersReturned_ReturnsSameValueInput()
        {
            //Setup
            var jsonService = Setup();
            var vouchersService = new VouchersService(jsonService.Object);

            //Action
            var result = await vouchersService.SubmitCouponAsync(3.60, "FAKEVOUCHER2");

            //Assert
            Assert.Equal(3.60, Math.Round(result, 2));
        }

        [Fact]
        public async Task GetVoucher_WithCorrectName_InDate_VouchersReturned_FixedValueApplied_ReturnsDiscountedValue()
        {
            //Setup
            var jsonService = Setup();
            var vouchersService = new VouchersService(jsonService.Object);

            //Action
            var result = await vouchersService.SubmitCouponAsync(20.55, "FAKEVOUCHER4");

            //Assert
            Assert.Equal(5.55, Math.Round(result, 2));
        }

        [Fact]
        public async Task GetVoucher_WithCorrectName_InDate_VouchersReturned_PercentageApplied_ReturnsDiscountedValue()
        {
            //Setup
            var jsonService = Setup();
            var vouchersService = new VouchersService(jsonService.Object);

            //Action
            var result = await vouchersService.SubmitCouponAsync(10, "FAKEVOUCHER1");

            //Assert
            Assert.Equal(7, Math.Round(result, 2));
        }

        [Fact]
        public async Task GetVoucher_WithCorrectName_InDate_VouchersReturned_FixedValueAppliedWithTotalCappedAtZero_ReturnsDiscountedValue()
        {
            //Setup
            var jsonService = Setup();
            var vouchersService = new VouchersService(jsonService.Object);

            //Action
            var result = await vouchersService.SubmitCouponAsync(5, "FAKEVOUCHER4");

            //Assert
            Assert.Equal(0, Math.Round(result, 2));
        }

        private Mock<IJsonService> Setup()
        {
            var jsonService = new Mock<IJsonService>();
            jsonService.Setup(x => x.LoadVouchersAsync()).ReturnsAsync(new System.Collections.Generic.List<Voucher>
            {
                new Voucher
                {
                    Id = 1,
                    DiscountCode = "FAKEVOUCHER1",
                    DiscountValue = "30%",
                    ValidFrom = "1995-09-24",
                    ValidTo = "2021-06-24"
                },
                new Voucher
                {
                    Id = 2,
                    DiscountCode = "FAKEVOUCHER2",
                    DiscountValue = "20",
                    ValidFrom = "2000-03-04",
                    ValidTo = "2013-01-04"
                },
                new Voucher
                {
                    Id = 3,
                    DiscountCode = "FAKEVOUCHER3",
                    DiscountValue = "10",
                    ValidFrom = "2021-08-13",
                    ValidTo = "2022-05-04"
                },
                new Voucher
                {
                    Id = 4,
                    DiscountCode = "FAKEVOUCHER4",
                    DiscountValue = "15",
                    ValidFrom = "1995-09-24",
                    ValidTo = "2021-06-24"
                }
            });

            return jsonService;
        }
    }
}
