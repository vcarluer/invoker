using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public abstract class Command
    {
        public Command(string name, char pattern)
        {
            this.Name = name;
            this.Pattern = pattern;
        }

        public string Name { get; private set; }
        public char Key
        {
            get
            {
                return KeyboardConfiguration.GetKey(this.GetType());
            }
        }
        public char Pattern { get; private set; }
    }
}
