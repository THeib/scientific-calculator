using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Scientific_Calculator.DataTier;


namespace Scientific_Calculator.AppTier
{
    public class Logger
    {

        public void addNewEntry(string log)
        {
            DateTime now = DateTime.Now;
            LoggerState.entities.Add(now + " : " + log);
        }

        public override string ToString()
        {
            var strings = from object o in LoggerState.entities
                          select o.ToString();
            return string.Join("\n", strings.ToArray());
        }

    }


}

