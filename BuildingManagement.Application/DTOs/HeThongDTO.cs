using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class HeThongDTO
    {
        public int MaHeThong { get; set; }
        public string TenHeThong { get; set; }
        public string NhanHieu { get; set; }
        public string Model { get; set; }
        public int TrangThai { get; set; }
        public string SerialNumber { get; set; }
        public string GhiChu { get; set; }
        public DateTime InstallationDate { get; set; } // Ngày lắp đặt
        public DateTime LastMaintenanceDate { get; set; } // Ngày bảo trì gần nhất
        public DateTime NextMaintenanceDate { get; set; } // Ngày bảo trì tiếp theo
        public int MaTN { get; set; }
        public string TenTN { get; set; }
    }

    public class CreateHeThong
    {
        public string TenHeThong { get; set; }
        public int MaTN { get; set; }
        public string GhiChu { get; set; }
        public string Model { get; set; }
        public string NhanHieu { get; set; }
        public string SerialNumber { get; set; }
        public int TrangThai { get; set; } // 0: Chưa sử dụng, 1: Đang sử dụng, 2: Bảo trì, 3: Hỏng hóc
    }

        public class UpdateHeThongDto
    {
        public int MaHeThong { get; set; }
        public string TenHeThong { get; set; }
        public int MaTN { get; set; }
        public string NhanHieu { get; set; }
        public int TrangThai { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayLapDat { get; set; } // Ngày lắp đặt
    }
}
