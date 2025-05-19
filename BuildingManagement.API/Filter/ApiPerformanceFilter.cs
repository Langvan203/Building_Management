using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace BuildingManagement.API.Filter
{
    public class ApiPerformanceFilter : IActionFilter
    {
        private readonly ILogger<ApiPerformanceFilter> _logger;
        private Stopwatch _stopwatch;

        public ApiPerformanceFilter(ILogger<ApiPerformanceFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context == null || _stopwatch == null)
            {
                return; // Hoặc log lỗi nếu cần
            }
            _stopwatch.Stop();
            var elapsedMs = _stopwatch.ElapsedMilliseconds;
            var action = context.ActionDescriptor?.DisplayName ?? "Unknown Action";
            _logger.LogInformation($"API {action} took {elapsedMs}ms to execute");
            context.HttpContext?.Response?.Headers?.Add("X-API-Response-Time", $"{elapsedMs}ms");
        }
    }
}
