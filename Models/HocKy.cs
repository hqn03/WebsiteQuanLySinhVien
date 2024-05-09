using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class HocKy
{
    public string MaHocKy { get; set; } = null!;

    public int? HocKy1 { get; set; }

    public string? NamHoc { get; set; }

    public virtual ICollection<LopHocPhanSinhVien> LopHocPhanSinhViens { get; set; } = new List<LopHocPhanSinhVien>();
}
