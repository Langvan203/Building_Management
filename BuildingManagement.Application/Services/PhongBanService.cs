using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildingManagement.Application.Services
{
    public class PhongBanService : IPhongBanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhongBanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatePhongBanDto> CreatePhongBanDto(CreatePhongBanDto dto, string tennv)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var checkPB = await _unitOfWork.PhongBans.GetFirstOrDefaultAsync(x => x.MaTN == dto.MaTN && x.TenPB == dto.TenPB);
                if (checkPB == null)
                {
                    var newPB = _mapper.Map<tnPhongBan>(dto);
                    newPB.NguoiTao = tennv;
                    await _unitOfWork.PhongBans.AddAsync(newPB);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                    return dto;
                }
                throw new Exception("Tên phòng ban đã tồn tại trong tòa nhà này");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi tạo phòng ban: " + ex.Message);
            }

        }

        public async Task<List<PhongBanDto>> GetAllPhongBan()
        {
            var dsPhongBan = await _unitOfWork.PhongBans.GetAllPhongBan();
            return dsPhongBan;
        }

        public Task<PhongBanDto> GetPhongBanById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveNhanVienInPhongBan(int maPB, int maNV)
        {
            var findNhanVienInPhongBan = await _unitOfWork.PhongBans.RemoveNhanVienInPhongBan(maPB, maNV);
            if (findNhanVienInPhongBan)
            {
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("Nhân viên không tồn tại trong phòng ban này");
            }
        }

        public async Task<bool> RemovePhongBan(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var phongBan = await _unitOfWork.PhongBans.GetByIdAsync(id);
                if (phongBan != null)
                {
                    await _unitOfWork.PhongBans.DeleteAsync(phongBan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                    return true;
                }
                throw new Exception("Phòng ban không tồn tại");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi xóa phòng ban: " + ex.Message);
            }

        }
        public async Task<bool> UpdatePhongBan(UpdateDatePhongBanDto dto, string tennv)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var phongBan = await _unitOfWork.PhongBans.GetByIdAsync(dto.MaPB);
                if (phongBan != null)
                {
                    phongBan.TenPB = dto.TenPB;
                    phongBan.MaTN = dto.MaTN;
                    phongBan.NguoiSua = tennv;
                    await _unitOfWork.PhongBans.UpdateAsync(phongBan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                    return true;
                }
                throw new Exception("Phòng ban không tồn tại");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi cập nhật phòng ban: " + ex.Message);
            }
        }
    }
}
