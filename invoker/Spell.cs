using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public class Spell
    {
        public Spell(string name, string pattern)
        {
            this.Name = name;
            this.Pattern = pattern;
        }

        public string Name { get; private set; }
        public string Pattern { get; private set; }
        public char[] PatternChar { get; private set; }

        public void ResetValidate()
        {
            this.PatternChar = this.Pattern.ToCharArray();
        }
        public bool PickChar(char c)
        {
            bool found = false;
            int pos = -1;
            int i = 0;
            foreach (char command in this.PatternChar)
            {
                if (command == c)
                {
                    pos = i;
                    break;
                }

                i++;
            }

            if (pos > -1)
            {
                found = true;
                this.PatternChar[pos] = ' ';
            }

            return found;
        }
    }
}
