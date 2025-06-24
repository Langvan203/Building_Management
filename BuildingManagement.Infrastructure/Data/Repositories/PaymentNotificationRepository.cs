using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class PaymentNotificationRepository : Repository<PaymentNotification>, IPaymentNotification
    {
        public PaymentNotificationRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public Task<PaymentNotification> GetByConditionUser(int userID)
        {
            throw new NotImplementedException();
        }
        // Additional methods specific to PaymentNotification can be added here
    }
}
