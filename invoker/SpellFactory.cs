using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public static class SpellFactory
    {
        public static Spell Create(string name, string pattern)
        {
            return new Spell(name, pattern);
        }
    }
}
