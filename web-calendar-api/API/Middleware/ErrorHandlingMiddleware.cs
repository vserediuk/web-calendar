using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using BusinessLogic.Exceptions;
using Serilog;
using Serilog.Events;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private const string MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        private ILogger _log = Serilog.Log.ForContext<ErrorHandlingMiddleware>();

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);
                sw.Stop();

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

                _log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : _log;
                _log.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode,
                    sw.Elapsed.TotalMilliseconds);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, sw, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Stopwatch sw, Exception e)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            LogException(httpContext, sw, e);
            var statusCode = e switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                BadRequestException _ => HttpStatusCode.BadRequest,
                ForbiddenException _ => HttpStatusCode.Forbidden,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new {error = e.Message});
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) statusCode;
            return httpContext.Response.WriteAsync(result);
        }

        private void LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
        {
            sw.Stop();

            LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500,
                    sw.Elapsed.TotalMilliseconds);
        }

        private ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = _log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                    destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm",
                    request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }
    }
}