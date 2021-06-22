using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer_Test.Services.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountValue { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}
