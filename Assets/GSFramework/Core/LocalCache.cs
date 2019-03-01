using UnityEngine;

namespace GSFramework
{
    public class LocalCache 
    {
        public static void SaveValue(string key, string valueToSave)
        {
            Debug.Log ("[Caching] "+key+" : "+valueToSave);

            PlayerPrefs.SetString (key, valueToSave);
        }

        public static string GetValue(string key)
        {
            return PlayerPrefs.HasKey (key) ? PlayerPrefs.GetString(key) : "";
        }

        public static void LoadCachedKeys()
        {
            TryLoadKey(Keys.UserName);
            TryLoadKey(Keys.Password);

        }

        private static void TryLoadKey(string key)
        {
            if (PlayerPrefs.HasKey (key))
                DataManager.Instance.Add_STRING (key,PlayerPrefs.GetString(key));
        }

    }
}


