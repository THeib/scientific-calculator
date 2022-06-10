using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Scientific_Calculator.Classes
{
    internal class Logger
    {

        public ArrayList entities = new ArrayList();
        public void addNewEntry(string log)
        {
            DateTime now = DateTime.Now;
            entities.Add(now + " : " + log);
        }
        
        public override string ToString()
        {
            var strings = from object o in entities
                          select o.ToString();
            return string.Join("\n", strings.ToArray());
        }

    }
}
