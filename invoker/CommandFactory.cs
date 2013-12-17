using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public static class CommandFactory
    {
        public static Command GetCommand(char key)
        {
            switch (key)
            {
                case 'a': return new Quas();
                case 'z': return new Wex();
                case 'e': return new Exort();
                case 'r': return new Ultimate();
                case 't': return new Cast();
                default: return null;
            }
        }
    }
}
