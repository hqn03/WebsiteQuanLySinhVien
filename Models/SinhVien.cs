using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class SinhVien
{
    public int MaSinhVien { get; set; }

    public string? HoTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? Cccd { get; set; }

    public string? LopSinhHoat { get; set; }

    public string? HinhAnhSinhVien { get; set; }

    public string? HinhAnhTheSinhVien { get; set; }

    public string? HinhAnhCccd { get; set; }

    public string? MatKhau { get; set; }

    public virtual ICollection<LopHocPhanSinhVien> LopHocPhanSinhViens { get; set; } = new List<LopHocPhanSinhVien>();

    public virtual LopSinhHoat? LopSinhHoatNavigation { get; set; }
}
