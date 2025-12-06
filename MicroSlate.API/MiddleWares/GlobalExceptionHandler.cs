using Ardalis.Result;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using System.Text.Json;

namespace MicroSlate.API.MiddleWares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _host;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
                IHostEnvironment host, IStringLocalizer<SharedResources> sharedLocalizer)
        {
            _logger = logger;
            _host = host;
            _sharedLocalizer = sharedLocalizer;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"Error occurred while processing the request Message:{exception.Message}, StackTrace:{exception.StackTrace}");
            var Message = Result.Error(_host.IsDevelopment() ? $"{exception?.InnerException?.Message}"
                : _sharedLocalizer["AnErrorOccurredPleaseContactSystemAdministrator"]);
            var list = new List<string>() { "localhost" };
            if (list.Contains(httpContext.Request.Host.Host))
            {
                Message = Result.Error($"Message : {exception?.InnerException?.Message}");
            }
            var serializedResponse = JsonSerializer.Serialize(Message);
            await httpContext.Response.WriteAsync(serializedResponse);
            return true;
        }

    }
}
