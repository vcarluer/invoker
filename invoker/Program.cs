using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    class Program
    {
        static int countOK = 0;
        static int countKO = 0;
        static long totalOK = 0;

        static void Main(string[] args)
        {
            var spells = new SpellCollection();
            Stopwatch watch = new Stopwatch();
            bool noValues = true;
            bool hint = false;
            Console.WriteLine("DOTA 2 - INVOKER TRAINING PROGRAM - LET'S ROX");
            Console.WriteLine("Quas ({0}) Wex ({1}) Exort ({2}) Ulti ({3}) Cast ({4})", "a", "z", "e", "r", "t");
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Modes:");
            Console.WriteLine("1 - pattern only");
            Console.WriteLine("2 - pattern and ulti (r)");
            Console.WriteLine("3 - pattern, ulti (r) and cast (t)");
            Console.Write("Choose a mode and hit ENTER: ");
            string modeStr = Console.ReadLine();
            Mode mode = Mode.pattern;
            if (modeStr == "1" || modeStr == "2" || modeStr == "3")
            {
                if (!Enum.TryParse<Mode>(modeStr, out mode))
                {
                    mode = Mode.pattern;
                }
            }

            string lastFailed = "";
            while (true)
            {
                bool ok = true;
                Console.Clear();
                Console.WriteLine("DOTA 2 - INVOKER TRAINING PROGRAM - LET'S ROX");
                Console.WriteLine("Quas ({0}) Wex ({1}) Exort ({2}) Ulti ({3}) Cast ({4})", "a", "z", "e", "r", "t");
                Console.WriteLine("              {0} MODE", mode.ToString().ToUpper());
                Console.WriteLine("--------------------------------------------");

                long avg = 0;
                if (countOK != 0)
                {
                    avg = totalOK / countOK;
                }

                string hintSw = "OFF";
                if (hint)
                {
                    hintSw = "ON";
                }

                string failed = "";
                if (lastFailed != "")
                {
                    failed = "failed " + lastFailed;
                }

                Console.WriteLine("[{0} / {1}ms] [{2}] e(x)it (h)int {3} {4}", countOK, avg, countKO, hintSw, failed);

                var spell = spells.GetRandom();
                spell.ResetValidate();
                Console.Write(spell.Name);
                if (hint)
                {
                    Console.Write(" ({0})", spell.Pattern);
                }

                Console.WriteLine("");

                int i = 0;

                noValues = false;
                watch.Reset();
                watch.Start();
                if (mode == Mode.pattern)
                {
                    while (i < spell.Pattern.Length)
                    {
                        char c = Console.ReadKey().KeyChar;
                        if (c == 'x')
                        {
                            return;
                        }

                        if (c == 'h')
                        {
                            hint = !hint;
                            noValues = true;
                            break;
                        }

                        if (!spell.PickChar(c))
                        {
                            ok = false;
                        }

                        i++;
                    }
                }

                if (mode == Mode.cast)
                {
                    char c = ' ';
                    string line = "";
                    string command = "";
                    while (c != 't')
                    {
                        c = Console.ReadKey().KeyChar;
                        if (c == 'x')
                        {
                            return;
                        }

                        if (c == 'h')
                        {
                            hint = !hint;
                            noValues = true;
                            break;
                        }

                        if (c == 'r')
                        {
                            if (line.Length >= 3)
                            {
                                command = line.Substring(line.Length - 3, 3);
                            }
                        }
                        else
                        {
                            line += c;
                        }
                    }

                    if (command == "")
                    {
                        ok = false;
                    }
                    else
                    {
                        var chars = command.ToCharArray();
                        foreach (var pick in chars)
                        {
                            if (!spell.PickChar(pick))
                            {
                                ok = false;
                                break;
                            }
                        }
                    }
                }

                if (mode == Mode.ulti)
                {
                    char c = ' ';
                    string line = "";
                    while (c != 'r')
                    {
                        c = Console.ReadKey().KeyChar;
                        if (c == 'x')
                        {
                            return;
                        }

                        if (c == 'h')
                        {
                            hint = !hint;
                            noValues = true;
                            break;
                        }

                        if (c != 'r')
                        {
                            line += c;
                        }
                    }

                    if (line.Length < 3)
                    {
                        ok = false;
                    }
                    else
                    {
                        string command = line.Substring(line.Length - 3, 3);
                        var chars = command.ToCharArray();
                        foreach (var pick in chars)
                        {
                            if (!spell.PickChar(pick))
                            {
                                ok = false;
                                break;
                            }
                        }
                    }
                }

                watch.Stop();

                if (!noValues)
                {
                    if (ok)
                    {
                        countOK++;
                        totalOK += watch.ElapsedMilliseconds;
                        lastFailed = "";
                    }
                    else
                    {
                        countKO++;
                        lastFailed = spell.Pattern;
                    }
                }

            }
        }
    }
}
