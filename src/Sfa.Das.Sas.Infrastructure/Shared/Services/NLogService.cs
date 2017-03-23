namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using NLog;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public class NLogService : ILog
    {
        private readonly IInfrastructureSettings _settings;

        private readonly string _loggerType;
        private readonly string _version;
        public string ApplicationName { get; set; }

        public NLogService(Type loggerType, IInfrastructureSettings settings)
        {
            _settings = settings;
            ApplicationName = ApplicationName ?? _settings.ApplicationName;
            _loggerType = loggerType?.ToString() ?? "DefaultIndexLogger";
            _version = GetVersion();
        }

        public void Trace(string message)
        {
            SendLog(message, NLog.LogLevel.Trace);
        }

        public void Trace(string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Trace, properties);
        }

        public void Trace(string message, ILogEntry logEntry)
        {
            SendLog(message, NLog.LogLevel.Trace, this.BuildProperties(logEntry));
        }

        public void Debug(string message)
        {
            SendLog(message, NLog.LogLevel.Debug);
        }

        public void Debug(string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Debug, properties);
        }

        public void Debug(string message, ILogEntry logEntry)
        {
            SendLog(message, LogLevel.Debug, new Dictionary<string, object> { { logEntry.GetType().Name, logEntry } });
        }

        public void Info(string message)
        {
            SendLog(message, NLog.LogLevel.Info);
        }

        public void Info(string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Info, properties);
        }

        public void Info(string message, ILogEntry logEntry)
        {
            SendLog(message, LogLevel.Info, new Dictionary<string, object> { { logEntry.GetType().Name, logEntry } });
        }

        public void Warn(string message)
        {
            SendLog(message, NLog.LogLevel.Warn);
        }

        public void Warn(string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Warn, properties);
        }

        public void Warn(string message, ILogEntry logEntry)
        {
            SendLog(message, LogLevel.Warn, new Dictionary<string, object> { { logEntry.GetType().Name, logEntry } });
        }

        public void Warn(Exception ex, string message)
        {
            SendLog(message, NLog.LogLevel.Warn, ex);
        }

        public void Warn(Exception ex, string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Warn, properties, ex);
        }

        public void Warn(Exception ex, string message, ILogEntry logEntry)
        {
            SendLog(message, NLog.LogLevel.Warn, BuildProperties(logEntry));
        }

        public void Error(Exception ex, string message)
        {
            SendLog(message, NLog.LogLevel.Error, ex);
        }

        public void Error(Exception ex, string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Error, properties, ex);
        }

        public void Error(Exception ex, string message, ILogEntry logEntry)
        {
            SendLog(message, NLog.LogLevel.Error, BuildProperties(logEntry), ex);
        }

        public void Fatal(Exception ex, string message)
        {
            SendLog(message, NLog.LogLevel.Fatal, ex);
        }

        public void Fatal(Exception ex, string message, IDictionary<string, object> properties)
        {
            SendLog(message, NLog.LogLevel.Fatal, properties, ex);
        }

        public void Fatal(Exception ex, string message, ILogEntry logEntry)
        {
            SendLog(message, NLog.LogLevel.Fatal, BuildProperties(logEntry), ex);
        }

        private string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }

        private IDictionary<string, object> BuildProperties(ILogEntry entry)
        {
            if (entry == null)
            {
                return null;
            }

            PropertyInfo[] properties = entry.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>(properties.Length);

            foreach (PropertyInfo propertyInfo in properties)
            {
                string name = propertyInfo.Name;
                ILogEntry logEntry = entry;
                object obj = propertyInfo.GetValue(logEntry);
                dictionary.Add(name, obj);
            }

            return dictionary;
        }

        private void SendLog(object message, LogLevel level, Exception exception = null)
        {
            SendLog(message, level, new Dictionary<string, object>(), exception);
        }

        private void SendLog(object message, LogLevel level, IDictionary<string, object> properties, Exception exception = null)
        {
            var propertiesLocal = new Dictionary<string, object>();
            if (properties != null)
            {
                propertiesLocal = new Dictionary<string, object>(properties);
            }

            if (!string.IsNullOrEmpty(ApplicationName))
            {
                propertiesLocal.Add("app_Name", ApplicationName);
            }
            propertiesLocal.Add("Environment", _settings.EnvironmentName);
            propertiesLocal.Add("LoggerType", _loggerType);
            propertiesLocal.Add("Version", _version);

            var logEvent = new LogEventInfo(level, _loggerType, message.ToString());

            if (exception != null)
            {
                propertiesLocal.Add("Exception", new { message = exception.Message, source = exception.Source, innerException = exception.InnerException, stackTrace = exception.StackTrace });
            }

            foreach (var property in propertiesLocal)
            {
                logEvent.Properties[property.Key] = property.Value;
            }

            ILogger log = LogManager.GetCurrentClassLogger();
            log.Log(logEvent);
        }
    }
}
