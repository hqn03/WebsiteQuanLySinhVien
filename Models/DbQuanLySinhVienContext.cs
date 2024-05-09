using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebsiteQuanLySinhVien.Models;

public partial class DbQuanLySinhVienContext : DbContext
{
    public DbQuanLySinhVienContext()
    {
    }

    public DbQuanLySinhVienContext(DbContextOptions<DbQuanLySinhVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GiaoVien> GiaoViens { get; set; }

    public virtual DbSet<HocKy> HocKies { get; set; }

    public virtual DbSet<HocPhan> HocPhans { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<LopHocPhan> LopHocPhans { get; set; }

    public virtual DbSet<LopHocPhanSinhVien> LopHocPhanSinhViens { get; set; }

    public virtual DbSet<LopSinhHoat> LopSinhHoats { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbQuanLySinhVien;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.MaGiaoVien).HasName("PK__GiaoVien__8D374F50DF78744B");

            entity.ToTable("GiaoVien");

            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Khoa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.KhoaNavigation).WithMany(p => p.GiaoViens)
                .HasForeignKey(d => d.Khoa)
                .HasConstraintName("FK__GiaoVien__Khoa__4CA06362");
        });

        modelBuilder.Entity<HocKy>(entity =>
        {
            entity.HasKey(e => e.MaHocKy).HasName("PK__HocKy__1EB551106D5BFC8B");

            entity.ToTable("HocKy");

            entity.Property(e => e.MaHocKy)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HocKy1).HasColumnName("HocKy");
            entity.Property(e => e.NamHoc)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HocPhan>(entity =>
        {
            entity.HasKey(e => e.MaHocPhan).HasName("PK__HocPhan__9A13F25E93838ED8");

            entity.ToTable("HocPhan");

            entity.Property(e => e.MaHocPhan)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HocPhanHocTruoc)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HocPhanSongHanh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HocPhanTienQuyet)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenHocPhan).HasMaxLength(100);
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__Khoa__65390405B5B7323E");

            entity.ToTable("Khoa");

            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenKhoa).HasMaxLength(100);
        });

        modelBuilder.Entity<LopHocPhan>(entity =>
        {
            entity.HasKey(e => e.MaLopHocPhan).HasName("PK__LopHocPh__82581CD9D9F5E8E2");

            entity.ToTable("LopHocPhan");

            entity.Property(e => e.MaLopHocPhan)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.MaHocPhan)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.GiaoVienNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.GiaoVien)
                .HasConstraintName("FK__LopHocPha__GiaoV__49C3F6B7");

            entity.HasOne(d => d.MaHocPhanNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.MaHocPhan)
                .HasConstraintName("FK__LopHocPha__MaHoc__4BAC3F29");
        });

        modelBuilder.Entity<LopHocPhanSinhVien>(entity =>
        {
            entity.HasKey(e => new { e.MaSinhVien, e.MaLopHocPhan, e.HocKy }).HasName("PK__LopHocPh__FE94D68A7EDE59A6");

            entity.ToTable("LopHocPhan_SinhVien");

            entity.Property(e => e.MaLopHocPhan)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.HocKy)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DiemChu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaDangKy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TrangThai).HasDefaultValue(0);

            entity.HasOne(d => d.HocKyNavigation).WithMany(p => p.LopHocPhanSinhViens)
                .HasForeignKey(d => d.HocKy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHocPha__HocKy__46E78A0C");

            entity.HasOne(d => d.MaLopHocPhanNavigation).WithMany(p => p.LopHocPhanSinhViens)
                .HasForeignKey(d => d.MaLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHocPha__MaLop__45F365D3");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.LopHocPhanSinhViens)
                .HasForeignKey(d => d.MaSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHocPha__MaSin__47DBAE45");
        });

        modelBuilder.Entity<LopSinhHoat>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK__LopSinhH__3B98D273C8825EC2");

            entity.ToTable("LopSinhHoat");

            entity.Property(e => e.MaLop)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.GiaoVienChuNhiemNavigation).WithMany(p => p.LopSinhHoats)
                .HasForeignKey(d => d.GiaoVienChuNhiem)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__LopSinhHo__GiaoV__48CFD27E");
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSinhVien).HasName("PK__SinhVien__939AE775527A7594");

            entity.ToTable("SinhVien");

            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.HinhAnhCccd)
                .IsUnicode(false)
                .HasColumnName("HinhAnhCCCD");
            entity.Property(e => e.HinhAnhSinhVien).IsUnicode(false);
            entity.Property(e => e.HinhAnhTheSinhVien).IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.LopSinhHoat)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.LopSinhHoatNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.LopSinhHoat)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SinhVien__LopSin__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
