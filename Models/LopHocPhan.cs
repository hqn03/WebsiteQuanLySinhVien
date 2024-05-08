using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class LopHocPhan
{
    public string MaLopHocPhan { get; set; } = null!;

    public string? MaHocPhan { get; set; }

    public int? GiaoVien { get; set; }

    public int? Thu { get; set; }

    public int? BatDau { get; set; }

    public int? KetThuc { get; set; }

    public int? SoLuongSinhVien { get; set; }

    public int? SoLuongToiDa { get; set; }

    public virtual GiaoVien? GiaoVienNavigation { get; set; }

    public virtual ICollection<LopHocPhanSinhVien> LopHocPhanSinhViens { get; set; } = new List<LopHocPhanSinhVien>();

    public virtual HocPhan? MaHocPhanNavigation { get; set; }
}
