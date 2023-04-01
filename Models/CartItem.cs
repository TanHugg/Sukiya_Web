using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sukiya.Models
{
    public class CartItem
    {
        public int MaMon { get; set; }
        [DisplayName("Ten Mon")]
        public string TenMon { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public decimal Money
        {
            get
            {
                return SoLuong * Gia;
            }
        }
    }
}
