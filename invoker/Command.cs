using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public class Command
    {
        public Command(string name, char key)
        {
            this.Name = name;
            this.Key = key;
            this.Pattern = key;
        }

        public string Name { get; private set; }
        public char Key { get; set; }
        public char Pattern { get; private set; }
    }
}
