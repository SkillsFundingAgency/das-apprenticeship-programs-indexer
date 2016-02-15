﻿namespace Sfa.Eds.Das.Infrastructure.Logging
{
    using System.Net;

    using log4net;
    using log4net.Appender;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;

    public static class Log4NetSettings
    {
        public static string LoggerName => "MainLogger";

        public static void Initialise(string logserver)
        {
            var logstashport = 5960;

            var appender = CreateUdpAppender("UdpAppender", logserver, logstashport);
            AddAppender(LoggerName, appender);

            var logger = LogManager.GetLogger(LoggerName);
            logger.Info($"Log appender added: {logserver}");
        }

        private static void AddAppender(string loggerName, IAppender appender)
        {
            ILog log = LogManager.GetLogger(loggerName);
            var l = log.Logger as Logger;

            l.Hierarchy.Root.AddAppender(appender);
            l.AddAppender(appender);
        }

        private static IAppender CreateUdpAppender(string name, string remoteip, int port)
        {
            var patternLayout = new PatternLayout { ConversionPattern = "%-5level %logger [%property{NDC}] - %message%newline" };
            patternLayout.ActivateOptions();

            var appender = new UdpAppender
            {
                Name = name,
                RemoteAddress = IPAddress.Parse(remoteip),
                RemotePort = port,
                Layout = patternLayout
            };
            appender.ActivateOptions();

            return appender;
        }
    }
}