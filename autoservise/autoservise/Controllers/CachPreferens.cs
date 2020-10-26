using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace autoservise.Controllers
{
    class CachPreferens
    {
        private static CachPreferens _instace = new CachPreferens();

        static internal CachPreferens GetInstance
        {
            get
            {
                return _instace;
            }
        }

        public bool HasKey(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public void SetKey(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public void SetKey(string key, bool value)
        {
            Preferences.Set(key, value);
        }

        public void SetKey(string key, int value)
        {
            Preferences.Set(key, value);
        }

        public string GetString(string key)
        {
            return Preferences.Get(key, "string");
        }

        public string GetInt(string key)
        {
            return Preferences.Get(key, "int");
        }

        public string GetBool(string key)
        {
            return Preferences.Get(key, "bool");
        }
    }
}
