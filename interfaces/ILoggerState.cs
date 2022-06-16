
using System.Collections.Generic;


namespace Scientific_Calculator.interfaces
{
    internal interface ILoggerState
    {

        List<string> getEntries();
        void addNewEntry(string value);

  
    }
}
