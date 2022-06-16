using System;
using Scientific_Calculator.interfaces;
using Scientific_Calculator.DataTier;
namespace Scientific_Calculator.AppTier
{
    internal class Logger : ILogger
    {
        private ILoggerState loggerState;

        private static Logger instance = null;

        private Logger()
        {
            loggerState = new LoggerState();

        }


        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }
        


        public ILoggerState getLogs()
        {

            return loggerState;

        }

        public void addNewlog(string log)
        {
            DateTime now = DateTime.Now;
            loggerState.addNewEntry(now + " > " +  log);

        }
    }

}