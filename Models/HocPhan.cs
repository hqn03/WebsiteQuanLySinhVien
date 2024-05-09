using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class HocPhan
{
    public string MaHocPhan { get; set; } = null!;

    public string? TenHocPhan { get; set; }

    public int? SoTinChi { get; set; }

    public string? HocPhanTienQuyet { get; set; }

    public string? HocPhanSongHanh { get; set; }

    public string? HocPhanHocTruoc { get; set; }

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();
}
