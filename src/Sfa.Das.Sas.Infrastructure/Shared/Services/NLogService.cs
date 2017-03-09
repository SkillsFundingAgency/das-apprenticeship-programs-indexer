using System.Diagnostics;
using System.Reflection;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using NLog;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public class NLogService : ILog
    {
        private readonly IInfrastructureSettings _settings;

        private readonly string _loggerType;
        private readonly string _version;

        public NLogService(Type loggerType, IInfrastructureSettings settings)
        {
            _settings = settings;
            ApplicationName = ApplicationName ?? _settings.ApplicationName;
            _loggerType = loggerType?.ToString() ?? "DefaultIndexLogger";
            _version = GetVersion();
        }

        public string ApplicationName { get; set; }


        public void Trace(string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Trace, (Exception)null);
        }

        public void Trace(string message, ILogEntry logEntry)
        {
            this.SendLog((object)message, NLog.LogLevel.Trace, this.BuildProperties(logEntry), (Exception)null);
        }

        public void Trace(string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Trace, properties, (Exception)null);
        }

        public void Debug(string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Debug, (Exception)null);
        }

        public void Debug(string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Debug, properties, (Exception)null);
        }

        public void Debug(string message, ILogEntry logEntry)
        {
            SendLog(message, LogLevel.Debug, new Dictionary<string, object> { { logEntry.GetType().Name, logEntry } });

        }

        public void Info(string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Info, (Exception)null);
        }

        public void Info(string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Info, properties, (Exception)null);
        }

        public void Info(string message, ILogEntry logEntry)
        {
            SendLog(message, LogLevel.Info, new Dictionary<string, object> { { logEntry.GetType().Name, logEntry } });

        }

        public void Warn(string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Warn, (Exception)null);
        }

        //public void Warn(string message, IDictionary<string, object> properties)
        //{
        //    throw new NotImplementedException();
        //}

        public void Warn(string message, ILogEntry logEntry)
        {
            SendLog(message, LogLevel.Warn, new Dictionary<string, object> { { logEntry.GetType().Name, logEntry } });

        }

        public void Warn(string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Warn, properties, (Exception)null);
        }

        public void Warn(Exception ex, string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Warn, ex);
        }

        public void Warn(Exception ex, string message, ILogEntry logEntry)
        {
            this.SendLog((object)message, NLog.LogLevel.Warn, this.BuildProperties(logEntry), (Exception)null);
        }

        public void Warn(Exception ex, string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Warn, properties, ex);
        }

        public void Error(Exception ex, string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Error, ex);
        }

        public void Error(Exception ex, string message, ILogEntry logEntry)
        {
            this.SendLog((object)message, NLog.LogLevel.Error, this.BuildProperties(logEntry), ex);
        }

        public void Error(Exception ex, string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Error, properties, ex);
        }

        public void Fatal(Exception ex, string message)
        {
            this.SendLog((object)message, NLog.LogLevel.Fatal, ex);
        }

        public void Fatal(Exception ex, string message, ILogEntry logEntry)
        {
            this.SendLog((object)message, NLog.LogLevel.Fatal, this.BuildProperties(logEntry), ex);
        }

        public void Fatal(Exception ex, string message, IDictionary<string, object> properties)
        {
            this.SendLog((object)message, NLog.LogLevel.Fatal, properties, ex);
        }



        public void Debug(object message)
        {
            SendLog(message, LogLevel.Debug);
        }

        public void Debug(string message, Dictionary<string, object> properties)
        {
            SendLog(message, LogLevel.Debug, properties);
        }

        //public void Debug(string message, ILogEntry entry)
        //{
        //    SendLog(message, LogLevel.Debug, new Dictionary<string, object> { { entry.GetType().Name, entry } });
        //}

        //public void Info(string message, ILogEntry entry)
        //{
        //    SendLog(message, LogLevel.Info, new Dictionary<string, object> { { entry.GetType().Name, entry } });
        //}

        public void Info(object message)
        {
            SendLog(message, LogLevel.Info);
        }

        public void Info(string message, Dictionary<string, object> properties)
        {
            SendLog(message, LogLevel.Info, properties);
        }

        public void Warn(object message)
        {
            SendLog(message, LogLevel.Warn);
        }

        public void Warn(string message, Dictionary<string, object> properties)
        {
            SendLog(message, LogLevel.Warn, properties);
        }

        //public void Warn(string message, ILogEntry entry)
        //{
        //    SendLog(message, LogLevel.Warn, new Dictionary<string, object> { { entry.GetType().Name, entry } });
        //}

        public void Warn(Exception exception, object message)
        {
            SendLog(message, LogLevel.Warn, exception);
        }

        public void Error(Exception exception, object message)
        {
            SendLog(message, LogLevel.Error, exception);
        }

        public void Error(string message)
        {
            SendLog(message, LogLevel.Error);
        }

        public void Error(string message, ILogEntry entry)
        {
            SendLog(message, LogLevel.Error, new Dictionary<string, object> { { entry.GetType().Name, entry } });
        }

        public void Fatal(Exception exception, object message)
        {
            SendLog(message, LogLevel.Fatal, exception);
        }

        public void Fatal(string message, ILogEntry entry)
        {
            SendLog(message, LogLevel.Fatal, new Dictionary<string, object> { { entry.GetType().Name, entry } });
        }

        private void SendLog(object message, LogLevel level, Exception exception = null)
        {
            SendLog(message, level, new Dictionary<string, object>(), exception);
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
                return (IDictionary<string, object>)null;
            PropertyInfo[] properties = entry.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>(properties.Length);
            foreach (PropertyInfo propertyInfo in properties)
            {
                string name = propertyInfo.Name;
                ILogEntry logEntry = entry;
                object obj = propertyInfo.GetValue((object)logEntry);
                dictionary.Add(name, obj);
            }
            return (IDictionary<string, object>)dictionary;
        }


        private void SendLog(object message, LogLevel level, IDictionary<string, object> properties, Exception exception = null)
        {
            var propertiesLocal = new Dictionary<string, object>();
            if (properties != null)
            {
                propertiesLocal = new Dictionary<string, object>(properties);
            }

            propertiesLocal.Add("Application", ApplicationName);
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
