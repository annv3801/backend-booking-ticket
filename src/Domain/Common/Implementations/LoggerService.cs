using System.Runtime.CompilerServices;
using Domain.Common.Interface;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;

namespace Domain.Common.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
       

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
         
        }


        /// <summary>
        /// Publish log event into the rabbitMQ
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        private void PublishLoggerEvent(Exception? exception, string message, string caller, string filePath, int lineNumber)
        {
            // var tracker = new Tracker(Guid.NewGuid().ToString());
            // // Get request id in http context
            // if (exception == null)
            // {
            //     tracker.RequestId = _httpContextAccessorService.GetRequestHeader(Constants.Tracker.Header.XRequestId) ?? tracker.RequestId;
            // }
            // // Get tracker in exception if any
            // if (exception!=null && exception.Data.Keys.Cast<string>().Any(key => key == "Tracker"))
            // {
            //     tracker = exception.Data["Tracker"] as Tracker;
            // }
            // // Init event
            // var @event = new LoggerEvent(exception, tracker ?? new Tracker(Guid.NewGuid().ToString()), message, caller, filePath, lineNumber);
            // // Publish event into rabbitMQ
            // _eventBus.Publish(@event);
        }

        public void LogFatal(Exception exception, string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(exception, message, caller, callerFilePath, callerLineNumber);
                Log.Fatal(exception, message, args);
            }
        }

        public void LogFatal(string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(null, message, caller, callerFilePath, callerLineNumber);
                Log.Fatal(message, args);
            }
        }


        public void LogDebug(Exception exception, string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                using (GlobalLogContext.Lock())
                {
                    GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                    GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                    GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                    PublishLoggerEvent(exception, message, caller, callerFilePath, callerLineNumber);
                    Log.Debug(exception, message, args);
                }
            }
        }

        public void LogDebug(string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                using (GlobalLogContext.Lock())
                {
                    GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                    GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                    GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                    PublishLoggerEvent(null, message, caller, callerFilePath, callerLineNumber);
                    Log.Debug(message, args);
                }
            }
        }


        public void LogError(Exception exception, string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(exception, message, caller, callerFilePath, callerLineNumber);
                Log.Error(exception, message, args);
            }
        }

        public void LogError(string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(null, message, caller, callerFilePath, callerLineNumber);
                Log.Error(message, args);
            }
        }


        public void LogInformation(Exception exception, string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(exception, message, caller, callerFilePath, callerLineNumber);
                Log.Information(exception, message, args);
            }
        }

        public void LogInformation(string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(null, message, caller, callerFilePath, callerLineNumber);
                Log.Information(message, args);
            }
        }


        public void LogWarning(Exception exception, string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(exception, message, caller, callerFilePath, callerLineNumber);
                Log.Warning(exception, message, args);
            }
        }

        public void LogWarning(string message, [CallerMemberName] string caller = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, params object[] args)
        {
            using (GlobalLogContext.Lock())
            {
                GlobalLogContext.PushProperty("CallerMemberName", caller, true);
                GlobalLogContext.PushProperty("CallerFilePath", callerFilePath, true);
                GlobalLogContext.PushProperty("CallerLineNumber", callerLineNumber, true);
                PublishLoggerEvent(null, message, caller, callerFilePath, callerLineNumber);
                Log.Warning(message, args);
            }
        }
    }
}