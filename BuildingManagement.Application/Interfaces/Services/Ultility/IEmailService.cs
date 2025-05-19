using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services.Ultility
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent, CancellationToken cancellationToken = default);
    }
}
