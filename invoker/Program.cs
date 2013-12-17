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
            bool continueGame = false;
            Mode mode = Mode.pattern;
            KeyboardConfiguration.SetAzerty();
            while (!continueGame)
            {
                continueGame = true;
                Console.Clear();
                Console.WriteLine("DOTA 2 - INVOKER TRAINING PROGRAM - LET'S ROX");
                Console.WriteLine("Quas ({0}) Wex ({1}) Exort ({2}) Ulti ({3}) Cast ({4})",
                                    KeyboardConfiguration.Quas,
                                    KeyboardConfiguration.Wex,
                                    KeyboardConfiguration.Exort,
                                    KeyboardConfiguration.Ultimate,
                                    KeyboardConfiguration.Cast);
                Console.WriteLine("");
                Console.WriteLine("-------@vcarluer---gamersassociate.com-------");
                Console.WriteLine("");
                Console.WriteLine("Modes:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("c - configure keyboard");
                Console.ResetColor();
                Console.WriteLine("1 - pattern only");
                Console.WriteLine("2 - pattern and ulti ({0})", KeyboardConfiguration.Ultimate);
                Console.WriteLine("3 - pattern, ulti ({0}) and cast ({1})", KeyboardConfiguration.Ultimate, KeyboardConfiguration.Cast);
                Console.Write("Choose a mode and hit ENTER: ");
                string modeStr = Console.ReadLine();
                if (modeStr.ToUpper().Equals("C"))
                {
                    Console.WriteLine("New keyboard configuration:");
                    Console.Write("Quas: ");
                    char q = Console.ReadKey().KeyChar;
                    Console.Write(Environment.NewLine);
                    Console.Write("Wex: ");
                    char w = Console.ReadKey().KeyChar;
                    Console.Write(Environment.NewLine);
                    Console.Write("Exort: ");
                    char e = Console.ReadKey().KeyChar;
                    Console.Write(Environment.NewLine);
                    Console.Write("Ultimate: ");
                    char r = Console.ReadKey().KeyChar;
                    Console.Write(Environment.NewLine);
                    Console.Write("Cast: ");
                    char t = Console.ReadKey().KeyChar;
                    Console.Write(Environment.NewLine);
                    KeyboardConfiguration.SetKeys(q, w, e, r, t);
                    continueGame = false;
                }
                else
                {
                    if (modeStr == "1" || modeStr == "2" || modeStr == "3")
                    {
                        if (!Enum.TryParse<Mode>(modeStr, out mode))
                        {
                            mode = Mode.pattern;
                        }
                    }
                }
            }

            string lastFailed = "";
            while (true)
            {
                bool ok = true;
                Console.Clear();
                Console.WriteLine("DOTA 2 - INVOKER TRAINING PROGRAM - LET'S ROX");
                Console.WriteLine("Quas ({0}) Wex ({1}) Exort ({2}) Ulti ({3}) Cast ({4})",
                                    KeyboardConfiguration.Quas,
                                    KeyboardConfiguration.Wex,
                                    KeyboardConfiguration.Exort,
                                    KeyboardConfiguration.Ultimate,
                                    KeyboardConfiguration.Cast);
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
                else
                {
                    failed = "success";
                }

                Console.ResetColor();
                Console.Write("[");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(countOK);
                Console.ResetColor();
                Console.Write(" / {0}ms] [", avg);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(countKO);
                Console.ResetColor();
                Console.Write("] ex");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("i");
                Console.ResetColor();
                Console.Write("t ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("h");
                Console.ResetColor();
                Console.Write("int {0} ", hintSw);
                if (lastFailed != "")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.Write(failed);
                Console.ResetColor();
                Console.Write(Environment.NewLine);
                Console.WriteLine("");

                var spell = spells.GetRandom();
                spell.ResetValidate();
                Console.Write(spell.Name);
                if (hint)
                {
                    Console.Write(" ({0})", spell.PatternKeyboard);
                }

                Console.WriteLine("");

                int i = 0;

                noValues = false;
                watch.Reset();
                watch.Start();
                if (mode == Mode.pattern)
                {
                    while (i < spell.PatternKeyboard.Length)
                    {
                        char c = Console.ReadKey().KeyChar;
                        if (c == KeyboardConfiguration.Exit)
                        {
                            return;
                        }

                        if (c == KeyboardConfiguration.Hint)
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
                    while (c != KeyboardConfiguration.Cast)
                    {
                        c = Console.ReadKey().KeyChar;
                        if (c == KeyboardConfiguration.Exit)
                        {
                            return;
                        }

                        if (c == KeyboardConfiguration.Hint)
                        {
                            hint = !hint;
                            noValues = true;
                            break;
                        }

                        if (c == KeyboardConfiguration.Ultimate)
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
                    while (c != KeyboardConfiguration.Ultimate)
                    {
                        c = Console.ReadKey().KeyChar;
                        if (c == KeyboardConfiguration.Exit)
                        {
                            return;
                        }

                        if (c == KeyboardConfiguration.Hint)
                        {
                            hint = !hint;
                            noValues = true;
                            break;
                        }

                        if (c != KeyboardConfiguration.Ultimate)
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
                        lastFailed = spell.PatternKeyboard;
                    }
                }

            }
        }
    }
}
