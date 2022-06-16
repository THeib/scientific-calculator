
using System.Collections.Generic;
using System.Linq;
using Scientific_Calculator.interfaces;

namespace Scientific_Calculator.DataTier
{
    public  class LoggerState: ILoggerState
    {
        private  List<string> entries;
       public  LoggerState()
        {

            entries = new List<string>();

        }

        public List<string> getEntries()
        {
            return entries;

        }

        public void addNewEntry(string value)
        {

            entries.Add(value);
        }

        public override string ToString()
        {
            var strings = from object o in entries
                          select o.ToString();
            return string.Join("\n", strings.ToArray());
        }

    }
}
