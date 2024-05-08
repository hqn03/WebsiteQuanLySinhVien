using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class GiaoVien
{
    public int MaGiaoVien { get; set; }

    public string? HoTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? Cccd { get; set; }

    public string? Khoa { get; set; }

    public string? MatKhau { get; set; }

    public virtual Khoa? KhoaNavigation { get; set; }

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();

    public virtual ICollection<LopSinhHoat> LopSinhHoats { get; set; } = new List<LopSinhHoat>();
}
