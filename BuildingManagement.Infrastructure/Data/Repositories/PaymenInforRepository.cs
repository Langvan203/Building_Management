using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class PaymenInforRepository : Repository<PaymentInfo>, IPaymenInforRepository
    {
        public PaymenInforRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public async Task<List<PaymentInfo>> GetByConditionIncludeTableHoaDon(Expression<Func<PaymentInfo, bool>> predicate)
        {
            var dsPaymentInfo = await _context.paymentInfo.Where(predicate)
                .Include(x => x.HoaDon).ThenInclude(x => x.tnKhachHang).ToListAsync();
            return dsPaymentInfo;
        }

        public async Task<PaymentInfo> GetByIdIncludeTable(string orderCode)
        {
            var paymentInfo = await _context.paymentInfo.Where(p => p.OrderCode == orderCode)
                .Include(x => x.HoaDon).FirstOrDefaultAsync();
            return paymentInfo;
        }

        public async Task<List<PaymentHistoryResponse>> GetHistoryPaymentByMaHD(int maHoaDon)
        {
            var paymentInfo = await _context.paymentInfo.Where(x => x.MaHD == maHoaDon).OrderByDescending(x => x.CreatedAt)
                .Select(x => new PaymentHistoryResponse
                {
                    OrderCode = x.OrderCode,
                    MaHD = x.MaHD,
                    Amount = x.Amount,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    PaidAt = x.PaidAt,
                    TransactionId = x.TransactionId
                }).ToListAsync();
            return paymentInfo;
        }
    }
}
