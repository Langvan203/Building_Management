using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponse> CreatePaymentLinkAsync(CreatePaymentRequest request);
        Task<PaymentStatusResponse> GetPaymentStatusAsync(string orderCode);
        Task<List<PaymentHistoryResponse>> GetPaymentHistoryAsync(int maHD);
        Task<PaymentInfo?> GetPaymentByOrderCodeAsync(string orderCode);
        Task<bool> UpdatePaymentStatusAsync(string orderCode, string status, string? transactionId = null);
        Task<bool> CancelPaymentAsync(string orderCode);
        Task<string> GeneratePaymentSignatureAsync(PayOSCreatePaymentRequest request);
        Task<bool> VerifyWebhookSignatureAsync(string webhookData, string signature);
    }
}
