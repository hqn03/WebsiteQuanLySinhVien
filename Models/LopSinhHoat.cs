using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class LopSinhHoat
{
    public string MaLop { get; set; } = null!;

    public int? GiaoVienChuNhiem { get; set; }

    public virtual GiaoVien? GiaoVienChuNhiemNavigation { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
