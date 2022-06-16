

namespace Scientific_Calculator.interfaces
{
    internal interface ILogger
    {
         void addNewlog(string log);
        ILoggerState getLogs();
    }
}
