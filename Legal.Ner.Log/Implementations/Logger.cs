using System;
using System.Reflection;
using log4net;

namespace Legal.Ner.Log.Implementations
{
    public class Logger : ILogger
    {
        private readonly ILog _log;

        public Logger()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }
    }
}
