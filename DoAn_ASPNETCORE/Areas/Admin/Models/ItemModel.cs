using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_ASPNETCORE.Areas.Admin.Models
{
    public class ItemModel
    {
        public  SanPhamModel SanPham { get; set; }
        public int QuanTity { get; set; }
    }
}
