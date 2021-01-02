using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn_ASPNETCORE.Migrations
{
    public partial class KhoiTao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucModel",
                columns: table => new
                {
                    ID_DanhMuc = table.Column<string>(nullable: false),
                    TenDanhMuc = table.Column<string>(nullable: true),
                    TrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucModel", x => x.ID_DanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCapModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNCC = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    TrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCapModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    HoTen = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Sdt = table.Column<string>(nullable: true),
                    Loai = table.Column<string>(nullable: true),
                    TrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSanPhamModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(maxLength: 50, nullable: false),
                    NhaCungCap = table.Column<int>(nullable: false),
                    TrangThai = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSanPhamModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoaiSanPhamModel_NhaCungCapModel_NhaCungCap",
                        column: x => x.NhaCungCap,
                        principalTable: "NhaCungCapModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(nullable: false),
                    HoTen = table.Column<string>(nullable: true),
                    Sdt = table.Column<string>(nullable: true),
                    ThanhTien = table.Column<int>(nullable: false),
                    TrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HoaDonModel_UserModel_User_ID",
                        column: x => x.User_ID,
                        principalTable: "UserModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSP = table.Column<string>(nullable: true),
                    MaLoai = table.Column<int>(nullable: false),
                    DanhMuc = table.Column<string>(nullable: true),
                    Gia = table.Column<int>(nullable: false),
                    GiaMoi = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Image_List = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    MoTaNgan = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    NgayLap = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SanPhamModel_DanhMucModel_DanhMuc",
                        column: x => x.DanhMuc,
                        principalTable: "DanhMucModel",
                        principalColumn: "ID_DanhMuc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SanPhamModel_LoaiSanPhamModel_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiSanPhamModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanModel",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(nullable: false),
                    SanPham_ID = table.Column<int>(nullable: false),
                    SanPhamID = table.Column<int>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true),
                    NgayDang = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_BinhLuanModel_SanPhamModel_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhamModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BinhLuanModel_UserModel_User_ID",
                        column: x => x.User_ID,
                        principalTable: "UserModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDonModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDon_ID = table.Column<int>(nullable: false),
                    SanPham_ID = table.Column<int>(nullable: false),
                    TenSP = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    Gia = table.Column<int>(nullable: false),
                    KhuyenMai = table.Column<int>(nullable: false),
                    ThanhTien = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDonModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDonModel_HoaDonModel_HoaDon_ID",
                        column: x => x.HoaDon_ID,
                        principalTable: "HoaDonModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDonModel_SanPhamModel_SanPham_ID",
                        column: x => x.SanPham_ID,
                        principalTable: "SanPhamModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanModel_SanPhamID",
                table: "BinhLuanModel",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanModel_User_ID",
                table: "BinhLuanModel",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDonModel_HoaDon_ID",
                table: "ChiTietHoaDonModel",
                column: "HoaDon_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDonModel_SanPham_ID",
                table: "ChiTietHoaDonModel",
                column: "SanPham_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonModel_User_ID",
                table: "HoaDonModel",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiSanPhamModel_NhaCungCap",
                table: "LoaiSanPhamModel",
                column: "NhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamModel_DanhMuc",
                table: "SanPhamModel",
                column: "DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamModel_MaLoai",
                table: "SanPhamModel",
                column: "MaLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuanModel");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDonModel");

            migrationBuilder.DropTable(
                name: "HoaDonModel");

            migrationBuilder.DropTable(
                name: "SanPhamModel");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropTable(
                name: "DanhMucModel");

            migrationBuilder.DropTable(
                name: "LoaiSanPhamModel");

            migrationBuilder.DropTable(
                name: "NhaCungCapModel");
        }
    }
}
