using System;
using System.Collections.Generic;

namespace WebsiteQuanLySinhVien.Models;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string? TenKhoa { get; set; }

    public virtual ICollection<GiaoVien> GiaoViens { get; set; } = new List<GiaoVien>();
}
