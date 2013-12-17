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
            Keys.Clear();
            SetKey(typeof(Quas), 'a');
            SetKey(typeof(Wex), 'z');
            SetKey(typeof(Exort), 'e');
            SetKey(typeof(Ultimate), 'r');
            SetKey(typeof(Cast), 't');
        }

        public static void SetQwerty()
        {
            Keys.Clear();
            SetKey(typeof(Quas), 'q');
            SetKey(typeof(Wex), 'w');
            SetKey(typeof(Exort), 'e');
            SetKey(typeof(Ultimate), 'r');
            SetKey(typeof(Cast), 't');
        }
    }
}
