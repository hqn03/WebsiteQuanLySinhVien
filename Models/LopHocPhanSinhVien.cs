using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class LopHocPhanSinhVien
{
    public int MaSinhVien { get; set; }

    public string MaLopHocPhan { get; set; } = null!;

    public string HocKy { get; set; } = null!;

    public double? Diem { get; set; }

    public string? DiemChu { get; set; }

    public string? MaDangKy { get; set; }

    public int? TrangThai { get; set; }

    public virtual HocKy HocKyNavigation { get; set; } = null!;

    public virtual LopHocPhan MaLopHocPhanNavigation { get; set; } = null!;

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
