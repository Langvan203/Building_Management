using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDemoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dvDienDinhMucs",
                columns: table => new
                {
                    MaDM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGiaDinhMuc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvDienDinhMucs", x => x.MaDM);
                });

            migrationBuilder.CreateTable(
                name: "dvgxLoaiXes",
                columns: table => new
                {
                    MaLX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvgxLoaiXes", x => x.MaLX);
                });

            migrationBuilder.CreateTable(
                name: "dvLoaiDVs",
                columns: table => new
                {
                    MaLDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLDV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvLoaiDVs", x => x.MaLDV);
                });

            migrationBuilder.CreateTable(
                name: "dvNuocDinhMucs",
                columns: table => new
                {
                    MaDM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGiaDinhMuc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvNuocDinhMucs", x => x.MaDM);
                });

            migrationBuilder.CreateTable(
                name: "mbLoaiMBs",
                columns: table => new
                {
                    MaLMB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLMB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mbLoaiMBs", x => x.MaLMB);
                });

            migrationBuilder.CreateTable(
                name: "nkbtTrangThais",
                columns: table => new
                {
                    MaTrangThai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nkbtTrangThais", x => x.MaTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tnKhachHangs",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    TaiKhoanCuDan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhauMaHoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCaNhan = table.Column<bool>(type: "bit", nullable: false),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiThuongTru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CtyTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnKhachHangs", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "tnNhanViens",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChiThuongTru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnNhanViens", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "tnPhongBans",
                columns: table => new
                {
                    MaPB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnPhongBans", x => x.MaPB);
                });

            migrationBuilder.CreateTable(
                name: "tnToaNhas",
                columns: table => new
                {
                    MaTN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTangNoi = table.Column<int>(type: "int", nullable: false),
                    SoTangHam = table.Column<int>(type: "int", nullable: false),
                    DienTichXayDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichSan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichChoThueNET = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichChoThueGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NganHangThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDungChuyenKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnToaNhas", x => x.MaTN);
                });

            migrationBuilder.CreateTable(
                name: "dvDichVus",
                columns: table => new
                {
                    MaDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLDV = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvDichVus", x => x.MaDV);
                    table.ForeignKey(
                        name: "FK_dvDichVus_dvLoaiDVs_MaLDV",
                        column: x => x.MaLDV,
                        principalTable: "dvLoaiDVs",
                        principalColumn: "MaLDV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_NhanVien",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_NhanVien", x => new { x.RoleID, x.MaNV });
                    table.ForeignKey(
                        name: "FK_Role_NhanVien_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_NhanVien_tnNhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "tnNhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan_NhanVien",
                columns: table => new
                {
                    MaPB = table.Column<int>(type: "int", nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan_NhanVien", x => new { x.MaPB, x.MaNV });
                    table.ForeignKey(
                        name: "FK_PhongBan_NhanVien_tnNhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "tnNhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongBan_NhanVien_tnPhongBans_MaPB",
                        column: x => x.MaPB,
                        principalTable: "tnPhongBans",
                        principalColumn: "MaPB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tnKhoiNhas",
                columns: table => new
                {
                    MaKN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaTN = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnKhoiNhas", x => x.MaKN);
                    table.ForeignKey(
                        name: "FK_tnKhoiNhas_tnToaNhas_MaTN",
                        column: x => x.MaTN,
                        principalTable: "tnToaNhas",
                        principalColumn: "MaTN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToaNha_NhanVien",
                columns: table => new
                {
                    MaTN = table.Column<int>(type: "int", nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToaNha_NhanVien", x => new { x.MaTN, x.MaNV });
                    table.ForeignKey(
                        name: "FK_ToaNha_NhanVien_tnNhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "tnNhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToaNha_NhanVien_tnToaNhas_MaTN",
                        column: x => x.MaTN,
                        principalTable: "tnToaNhas",
                        principalColumn: "MaTN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tnTangLaus",
                columns: table => new
                {
                    MaTL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienTichSan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichKhuVucDungChung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichKyThuaPhuTro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaKN = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnTangLaus", x => x.MaTL);
                    table.ForeignKey(
                        name: "FK_tnTangLaus_tnKhoiNhas_MaKN",
                        column: x => x.MaKN,
                        principalTable: "tnKhoiNhas",
                        principalColumn: "MaKN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tnMatBangs",
                columns: table => new
                {
                    MaMB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaVT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienTichBG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichThongThuy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichTimTuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBanGiao = table.Column<bool>(type: "bit", nullable: false),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBanGiao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHanChoThue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaTL = table.Column<int>(type: "int", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: true),
                    MaLMB = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnMatBangs", x => x.MaMB);
                    table.ForeignKey(
                        name: "FK_tnMatBangs_mbLoaiMBs_MaLMB",
                        column: x => x.MaLMB,
                        principalTable: "mbLoaiMBs",
                        principalColumn: "MaLMB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tnMatBangs_tnKhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "tnKhachHangs",
                        principalColumn: "MaKH");
                    table.ForeignKey(
                        name: "FK_tnMatBangs_tnTangLaus_MaTL",
                        column: x => x.MaTL,
                        principalTable: "tnTangLaus",
                        principalColumn: "MaTL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvDichVuSuDungs",
                columns: table => new
                {
                    MaDVSD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KyThanhToan = table.Column<int>(type: "int", nullable: false),
                    IsThanhToanTheoKy = table.Column<bool>(type: "bit", nullable: false),
                    NgayBatDauTinhPhi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThucTinhPhi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TyLeVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDuyet = table.Column<bool>(type: "bit", nullable: false),
                    TyLeBVMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienBVMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaDV = table.Column<int>(type: "int", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaMB = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvDichVuSuDungs", x => x.MaDVSD);
                    table.ForeignKey(
                        name: "FK_dvDichVuSuDungs_dvDichVus_MaDV",
                        column: x => x.MaDV,
                        principalTable: "dvDichVus",
                        principalColumn: "MaDV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvDichVuSuDungs_tnKhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "tnKhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvDichVuSuDungs_tnMatBangs_MaMB",
                        column: x => x.MaMB,
                        principalTable: "tnMatBangs",
                        principalColumn: "MaMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvDienDongHos",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoDongHo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaMB = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvDienDongHos", x => x.MaDH);
                    table.ForeignKey(
                        name: "FK_dvDienDongHos_tnMatBangs_MaMB",
                        column: x => x.MaMB,
                        principalTable: "tnMatBangs",
                        principalColumn: "MaMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvgxTheXes",
                columns: table => new
                {
                    MaTX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayBatDauSuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHanSuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaLX = table.Column<int>(type: "int", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaMB = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvgxTheXes", x => x.MaTX);
                    table.ForeignKey(
                        name: "FK_dvgxTheXes_dvgxLoaiXes_MaLX",
                        column: x => x.MaLX,
                        principalTable: "dvgxLoaiXes",
                        principalColumn: "MaLX",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvgxTheXes_tnKhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "tnKhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvgxTheXes_tnMatBangs_MaMB",
                        column: x => x.MaMB,
                        principalTable: "tnMatBangs",
                        principalColumn: "MaMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvNuocDongHos",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaMB = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvNuocDongHos", x => x.MaDH);
                    table.ForeignKey(
                        name: "FK_dvNuocDongHos_tnMatBangs_MaMB",
                        column: x => x.MaMB,
                        principalTable: "tnMatBangs",
                        principalColumn: "MaMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mbTrangThais",
                columns: table => new
                {
                    MaTrangThai = table.Column<int>(type: "int", nullable: false),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mbTrangThais", x => x.MaTrangThai);
                    table.ForeignKey(
                        name: "FK_mbTrangThais_tnMatBangs_MaTrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "tnMatBangs",
                        principalColumn: "MaMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tnbtHeThongs",
                columns: table => new
                {
                    MaHeThong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHeThong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaMB = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnbtHeThongs", x => x.MaHeThong);
                    table.ForeignKey(
                        name: "FK_tnbtHeThongs_tnMatBangs_MaMB",
                        column: x => x.MaMB,
                        principalTable: "tnMatBangs",
                        principalColumn: "MaMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvHoaDons",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThueVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThueBVMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienTruocVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DaThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConNo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienBVMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhaiThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaDVSD = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvHoaDons", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_dvHoaDons_dvDichVuSuDungs_MaDVSD",
                        column: x => x.MaDVSD,
                        principalTable: "dvDichVuSuDungs",
                        principalColumn: "MaDVSD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvDiens",
                columns: table => new
                {
                    MaDien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiSoDau = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChiSoCuoi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTieuThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    MaDH = table.Column<int>(type: "int", nullable: false),
                    MaDM = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvDiens", x => x.MaDien);
                    table.ForeignKey(
                        name: "FK_dvDiens_dvDienDinhMucs_MaDM",
                        column: x => x.MaDM,
                        principalTable: "dvDienDinhMucs",
                        principalColumn: "MaDM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvDiens_dvDienDongHos_MaDH",
                        column: x => x.MaDH,
                        principalTable: "dvDienDongHos",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dvNuocs",
                columns: table => new
                {
                    MaNuoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiSoDau = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChiSoCuoi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTieuThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    MaDM = table.Column<int>(type: "int", nullable: false),
                    MaDH = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvNuocs", x => x.MaNuoc);
                    table.ForeignKey(
                        name: "FK_dvNuocs_dvNuocDinhMucs_MaDM",
                        column: x => x.MaDM,
                        principalTable: "dvNuocDinhMucs",
                        principalColumn: "MaDM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dvNuocs_dvNuocDongHos_MaDH",
                        column: x => x.MaDH,
                        principalTable: "dvNuocDongHos",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nkbtKeHoachBaoTris",
                columns: table => new
                {
                    MaKeHoach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKeHoach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHeThong = table.Column<int>(type: "int", nullable: false),
                    MaTrangThai = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nkbtKeHoachBaoTris", x => x.MaKeHoach);
                    table.ForeignKey(
                        name: "FK_nkbtKeHoachBaoTris_nkbtTrangThais_MaTrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "nkbtTrangThais",
                        principalColumn: "MaTrangThai",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nkbtKeHoachBaoTris_tnbtHeThongs_MaHeThong",
                        column: x => x.MaHeThong,
                        principalTable: "tnbtHeThongs",
                        principalColumn: "MaHeThong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nkbtLichSuBaoTris",
                columns: table => new
                {
                    MaLichSu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GhiChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHeThong = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nkbtLichSuBaoTris", x => x.MaLichSu);
                    table.ForeignKey(
                        name: "FK_nkbtLichSuBaoTris_tnbtHeThongs_MaHeThong",
                        column: x => x.MaHeThong,
                        principalTable: "tnbtHeThongs",
                        principalColumn: "MaHeThong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tnycYeuCauSuaChuas",
                columns: table => new
                {
                    MaYC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiYeuCau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHeThong = table.Column<int>(type: "int", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnycYeuCauSuaChuas", x => x.MaYC);
                    table.ForeignKey(
                        name: "FK_tnycYeuCauSuaChuas_tnKhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "tnKhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tnycYeuCauSuaChuas_tnbtHeThongs_MaHeThong",
                        column: x => x.MaHeThong,
                        principalTable: "tnbtHeThongs",
                        principalColumn: "MaHeThong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ptPhieuThus",
                columns: table => new
                {
                    MaPT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiThu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHD = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ptPhieuThus", x => x.MaPT);
                    table.ForeignKey(
                        name: "FK_ptPhieuThus_dvHoaDons_MaHD",
                        column: x => x.MaHD,
                        principalTable: "dvHoaDons",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoTri_NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    MaKeHoach = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoTri_NhanVien", x => new { x.MaNV, x.MaKeHoach });
                    table.ForeignKey(
                        name: "FK_BaoTri_NhanVien_nkbtKeHoachBaoTris_MaKeHoach",
                        column: x => x.MaKeHoach,
                        principalTable: "nkbtKeHoachBaoTris",
                        principalColumn: "MaKeHoach",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaoTri_NhanVien_tnNhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "tnNhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nkbtChiTietBaoTris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKeHoach = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiSua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nkbtChiTietBaoTris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nkbtChiTietBaoTris_nkbtKeHoachBaoTris_MaKeHoach",
                        column: x => x.MaKeHoach,
                        principalTable: "nkbtKeHoachBaoTris",
                        principalColumn: "MaKeHoach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaoTri_NhanVien_MaKeHoach",
                table: "BaoTri_NhanVien",
                column: "MaKeHoach");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVus_MaLDV",
                table: "dvDichVus",
                column: "MaLDV");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVuSuDungs_MaDV",
                table: "dvDichVuSuDungs",
                column: "MaDV");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVuSuDungs_MaKH",
                table: "dvDichVuSuDungs",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVuSuDungs_MaMB",
                table: "dvDichVuSuDungs",
                column: "MaMB");

            migrationBuilder.CreateIndex(
                name: "IX_dvDienDongHos_MaMB",
                table: "dvDienDongHos",
                column: "MaMB",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dvDiens_MaDH",
                table: "dvDiens",
                column: "MaDH");

            migrationBuilder.CreateIndex(
                name: "IX_dvDiens_MaDM",
                table: "dvDiens",
                column: "MaDM");

            migrationBuilder.CreateIndex(
                name: "IX_dvgxTheXes_MaKH",
                table: "dvgxTheXes",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_dvgxTheXes_MaLX",
                table: "dvgxTheXes",
                column: "MaLX");

            migrationBuilder.CreateIndex(
                name: "IX_dvgxTheXes_MaMB",
                table: "dvgxTheXes",
                column: "MaMB");

            migrationBuilder.CreateIndex(
                name: "IX_dvHoaDons_MaDVSD",
                table: "dvHoaDons",
                column: "MaDVSD");

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocDongHos_MaMB",
                table: "dvNuocDongHos",
                column: "MaMB",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocs_MaDH",
                table: "dvNuocs",
                column: "MaDH");

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocs_MaDM",
                table: "dvNuocs",
                column: "MaDM");

            migrationBuilder.CreateIndex(
                name: "IX_nkbtChiTietBaoTris_MaKeHoach",
                table: "nkbtChiTietBaoTris",
                column: "MaKeHoach");

            migrationBuilder.CreateIndex(
                name: "IX_nkbtKeHoachBaoTris_MaHeThong",
                table: "nkbtKeHoachBaoTris",
                column: "MaHeThong");

            migrationBuilder.CreateIndex(
                name: "IX_nkbtKeHoachBaoTris_MaTrangThai",
                table: "nkbtKeHoachBaoTris",
                column: "MaTrangThai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nkbtLichSuBaoTris_MaHeThong",
                table: "nkbtLichSuBaoTris",
                column: "MaHeThong");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBan_NhanVien_MaNV",
                table: "PhongBan_NhanVien",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ptPhieuThus_MaHD",
                table: "ptPhieuThus",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_Role_NhanVien_MaNV",
                table: "Role_NhanVien",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_tnbtHeThongs_MaMB",
                table: "tnbtHeThongs",
                column: "MaMB");

            migrationBuilder.CreateIndex(
                name: "IX_tnKhoiNhas_MaTN",
                table: "tnKhoiNhas",
                column: "MaTN");

            migrationBuilder.CreateIndex(
                name: "IX_tnMatBangs_MaKH",
                table: "tnMatBangs",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_tnMatBangs_MaLMB",
                table: "tnMatBangs",
                column: "MaLMB");

            migrationBuilder.CreateIndex(
                name: "IX_tnMatBangs_MaTL",
                table: "tnMatBangs",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "IX_tnTangLaus_MaKN",
                table: "tnTangLaus",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_MaHeThong",
                table: "tnycYeuCauSuaChuas",
                column: "MaHeThong");

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_MaKH",
                table: "tnycYeuCauSuaChuas",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_ToaNha_NhanVien_MaNV",
                table: "ToaNha_NhanVien",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoTri_NhanVien");

            migrationBuilder.DropTable(
                name: "dvDiens");

            migrationBuilder.DropTable(
                name: "dvgxTheXes");

            migrationBuilder.DropTable(
                name: "dvNuocs");

            migrationBuilder.DropTable(
                name: "mbTrangThais");

            migrationBuilder.DropTable(
                name: "nkbtChiTietBaoTris");

            migrationBuilder.DropTable(
                name: "nkbtLichSuBaoTris");

            migrationBuilder.DropTable(
                name: "PhongBan_NhanVien");

            migrationBuilder.DropTable(
                name: "ptPhieuThus");

            migrationBuilder.DropTable(
                name: "Role_NhanVien");

            migrationBuilder.DropTable(
                name: "tnycYeuCauSuaChuas");

            migrationBuilder.DropTable(
                name: "ToaNha_NhanVien");

            migrationBuilder.DropTable(
                name: "dvDienDinhMucs");

            migrationBuilder.DropTable(
                name: "dvDienDongHos");

            migrationBuilder.DropTable(
                name: "dvgxLoaiXes");

            migrationBuilder.DropTable(
                name: "dvNuocDinhMucs");

            migrationBuilder.DropTable(
                name: "dvNuocDongHos");

            migrationBuilder.DropTable(
                name: "nkbtKeHoachBaoTris");

            migrationBuilder.DropTable(
                name: "tnPhongBans");

            migrationBuilder.DropTable(
                name: "dvHoaDons");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "tnNhanViens");

            migrationBuilder.DropTable(
                name: "nkbtTrangThais");

            migrationBuilder.DropTable(
                name: "tnbtHeThongs");

            migrationBuilder.DropTable(
                name: "dvDichVuSuDungs");

            migrationBuilder.DropTable(
                name: "dvDichVus");

            migrationBuilder.DropTable(
                name: "tnMatBangs");

            migrationBuilder.DropTable(
                name: "dvLoaiDVs");

            migrationBuilder.DropTable(
                name: "mbLoaiMBs");

            migrationBuilder.DropTable(
                name: "tnKhachHangs");

            migrationBuilder.DropTable(
                name: "tnTangLaus");

            migrationBuilder.DropTable(
                name: "tnKhoiNhas");

            migrationBuilder.DropTable(
                name: "tnToaNhas");
        }
    }
}
