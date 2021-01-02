using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_ASPNETCORE.Areas.Admin.Models
{
    public class SanPhamModel
    {
        public int ID { get; set; }
        public string TenSP { get; set; }
        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public virtual LoaiSanPhamModel Loai { set; get; }

        public string DanhMuc { get; set; }
        [ForeignKey("DanhMuc")]
        public virtual DanhMucModel DMuc { get; set; }
        public int Gia { get; set; }
        public int GiaMoi { get; set; }
        public string Image { get; set; }
        public string Image_List { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public string MoTaNgan { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayLap { get; set; }
        public string TrangThai { get; set; }
        public ICollection<ChiTietHoaDonModel> lstChiTietHoaDon { set; get; }

       
    }
}
