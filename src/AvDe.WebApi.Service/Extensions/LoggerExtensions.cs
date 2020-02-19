using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Runtime.CompilerServices;

namespace AvDe.WebApi.Service.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogAppError<T>(this ILogger<T> logger, string message, Exception exception = null, string ipAddress = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using (var prop = LogContext.PushProperty("MemberName", memberName))
            {
                LogContext.PushProperty("FilePath", sourceFilePath);
                LogContext.PushProperty("LineNumber", sourceLineNumber);
                LogContext.PushProperty("IpAddress", ipAddress);
                logger.LogError(exception, message);
            }
        }

        public static void LogAppInformation<T>(this ILogger<T> logger, string message, string ipAddress = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using (var prop = LogContext.PushProperty("MemberName", memberName))
            {
                LogContext.PushProperty("FilePath", sourceFilePath);
                LogContext.PushProperty("LineNumber", sourceLineNumber);
                LogContext.PushProperty("IpAddress", ipAddress);
                logger.LogInformation(message);
            }
        }

        public static void LogAppWarning<T>(this ILogger<T> logger, string message, string ipAddress = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using (var prop = LogContext.PushProperty("MemberName", memberName))
            {
                LogContext.PushProperty("FilePath", sourceFilePath);
                LogContext.PushProperty("LineNumber", sourceLineNumber);
                LogContext.PushProperty("IpAddress", ipAddress);
                logger.LogWarning(message);
            }
        }
    }
}
