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
            this.PatternCommands = new Command[pattern.Length];
            int i = 0;
            foreach (char command in this.Pattern.ToCharArray())
            {
                this.PatternCommands[i] = CommandFactory.GetCommand(command);
                i++;
            }
        }

        public string Name { get; private set; }
        private string Pattern { get; set; }
        private char[] PatternValidate { get; set; }
        private Command[] PatternCommands { get; set; }

        public string PatternKeyboard
        {
            get
            {
                string kp = String.Empty;
                foreach (Command command in this.PatternCommands)
                {
                    kp += command.Key;
                }

                return kp;
            }
        }

        public void ResetValidate()
        {
            this.PatternValidate = new char[this.PatternCommands.Length];
            int i = 0;
            foreach (Command command in this.PatternCommands)
            {
                this.PatternValidate[i] = command.Key;
                i++;
            }
        }
        public bool PickChar(char c)
        {
            bool found = false;
            int pos = -1;
            int i = 0;
            foreach (char command in this.PatternValidate)
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
                this.PatternValidate[pos] = ' ';
            }

            return found;
        }
    }
}
