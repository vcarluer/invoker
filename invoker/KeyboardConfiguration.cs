using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public static class KeyboardConfiguration
    {
        private static IDictionary<Type, char> Keys = new Dictionary<Type, char>();

        public static char Exit
        {
            get
            {
                return 'i';
            }
        }

        public static char Hint
        {
            get
            {
                return 'h';
            }
        }

        public static char Quas
        {
            get
            {
                return Keys[typeof(Quas)];
            }
        }

        public static char Wex
        {
            get
            {
                return Keys[typeof(Wex)];
            }
        }

        public static char Exort
        {
            get
            {
                return Keys[typeof(Exort)];
            }
        }

        public static char Ultimate
        {
            get
            {
                return Keys[typeof(Ultimate)];
            }
        }

        public static char Cast
        {
            get
            {
                return Keys[typeof(Cast)];
            }
        }

        public static void SetKey(Type type, char key)
        {
            Keys.Add(type, key);
        }

        public static char GetKey(Type type)
        {
            return Keys[type];
        }

        public static void SetAzerty()
        {
            SetKeys('a', 'z', 'e', 'r', 't');
        }

        public static void SetQwerty()
        {
            SetKeys('q', 'w', 'e', 'r', 't');
        }

        public static void SetKeys(char q, char w, char e, char r, char t)
        {
            Keys.Clear();
            SetKey(typeof(Quas), q);
            SetKey(typeof(Wex), w);
            SetKey(typeof(Exort), e);
            SetKey(typeof(Ultimate), r);
            SetKey(typeof(Cast), t);
        }
    }
}
