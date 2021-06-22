using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer_Test.Services.Models
{
    public class Basket
    {
        public List<Item> Items { get; set; }
        public int Total { get; set; }
        public Voucher Voucher { get; set; } = null;
    }
}
