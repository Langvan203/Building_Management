using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuSuDungController : BaseApiController
    {
        private readonly IDichVuSuDungSerivce _dichVuSuDungSerivce;

        public DichVuSuDungController(IDichVuSuDungSerivce dichVuSuDungSerivce)
        {
            _dichVuSuDungSerivce = dichVuSuDungSerivce;
        }

        [HttpGet("GetDSYeuCauSuDung")]
        public async Task<IActionResult> GetDSYeuCauSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15)
        {
            var result = await _dichVuSuDungSerivce.GetDSYeuCauSuDung(pageNumber, ngayBatDau, ngayKetThuc, pageSize);
            return Ok(result);
        }

        [HttpPost("DuyetYeuCau")]
        public async Task<IActionResult> DuyetYeuCau(int MaDVSD)
        {
            var result = await _dichVuSuDungSerivce.DuyetYeuCau(MaDVSD);
            if (result)
            {
                return Ok("Yêu cầu đã được duyệt thành công.");
            }
            return NotFound("Không tìm thấy yêu cầu dịch vụ sử dụng.");
        }

        [HttpPost("TuChoiYeuCau")]
        public async Task<IActionResult> TuChoiYeuCau(int MaDVSD)
        {
            var result = await _dichVuSuDungSerivce.TuChoiYeuCau(MaDVSD);
            if (result)
            {
                return Ok("Yêu cầu đã được từ chối thành công.");
            }
            return NotFound("Không tìm thấy yêu cầu dịch vụ sử dụng.");
        }

        [HttpGet("GetDSDangSuDung")]
        public async Task<IActionResult> GetDSDangSuDung(int pageNumber, int pageSize = 15)
        {
            var result = await _dichVuSuDungSerivce.GetDSDangSuDung(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("CreateDichVuSuDung")]
        public async Task<IActionResult> CreateDichVuSuDung([FromBody] CreateDichVuSuDungDto createDichVuSuDungDto)
        {
            var result = await _dichVuSuDungSerivce.CreateDichVuSuDung(createDichVuSuDungDto, Name);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Đăng ký dịch vụ sử dụng đã tồn tại hoặc thông tin không hợp lệ.");
        }

        [HttpPost("NgungSuDung")]
        public async Task<IActionResult> NgungSuDung(int MaDVSD)
        {
            var result = await _dichVuSuDungSerivce.NgungSuDung(MaDVSD);
            if (result)
            {
                return Ok("Dịch vụ sử dụng đã được ngừng thành công.");
            }
            return NotFound("Không tìm thấy dịch vụ sử dụng với mã này.");
        }

        [HttpPost("TiepTucSuDung")]
        public async Task<IActionResult> TiepTucSuDung(int MaDVSD)
        {
            var result = await _dichVuSuDungSerivce.TiepTucSuDung(MaDVSD);
            if (result)
            {
                return Ok("Dịch vụ sử dụng đã được tiếp tục thành công.");
            }
            return NotFound("Không tìm thấy dịch vụ sử dụng với mã này.");
        }

        [HttpGet("GetThongKeSuDung")]
        public async Task<IActionResult> GetThongKeSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15)
        {
            var result = await _dichVuSuDungSerivce.GetThongKeSuDung(pageNumber, ngayBatDau, ngayKetThuc, pageSize);
            return Ok(result);
        }

        [HttpGet("ExportThongKeToExcel")]
        public async Task<IActionResult> ExportThongKeToExcel(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var fileContent = await _dichVuSuDungSerivce.ExportThongKeToExcel(ngayBatDau, ngayKetThuc);
            if (fileContent != null)
            {
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ThongKeSuDung.xlsx");
            }
            return BadRequest("Không có dữ liệu để xuất.");
        }
        [HttpPost("DuyetSangHoaDon")]
        public async Task<IActionResult> DuyetSangHoaDon(int MaDVSD)
        {
            var result = await _dichVuSuDungSerivce.DuyetSangHoaDon(MaDVSD, Name);
            if (result)
            {
                return Ok("Đã duyệt sang hóa đơn thành công.");
            }
            return NotFound("Không tìm thấy dịch vụ sử dụng với mã này.");
        }
    }
}
