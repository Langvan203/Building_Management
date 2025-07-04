﻿using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IPaymenInforRepository : IRepository<PaymentInfo>
    {
        Task<PaymentInfo> GetByIdIncludeTable(string orderCode);
        Task<List<PaymentInfo>> GetByConditionIncludeTableHoaDon(Expression<Func<PaymentInfo, bool>> predicate);
        Task<List<PaymentHistoryResponse>> GetHistoryPaymentByMaHD(int maHoaDon);
    }
}
