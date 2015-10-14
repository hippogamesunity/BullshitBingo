using System;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts
{
    public class Profile
    {
        public bool Mute;
        
        private static Profile _instance;
        private const string ProfileKey = "T";

        public static Profile Instance
        {
            get { return _instance ?? Load(); }
        }

        public static Profile Load()
        {
            try
            {
                return _instance = PlayerPrefs.HasKey(ProfileKey) ? FromJson(JSONNode.LoadFromCompressedBase64(PlayerPrefs.GetString(ProfileKey))) : DefaultProfile;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);

                return _instance = DefaultProfile;
            }
        }

        private static Profile DefaultProfile
        {
            get
            {
                return new Profile
                {
                    Mute = false
                };
            }
        }

        public void Save()
        {
            PlayerPrefs.SetString(ProfileKey, ToJson().SaveToCompressedBase64());
            PlayerPrefs.Save();

            #if UNITY_EDITOR

            var json = ToJson();

            Debug.Log(json.ToString());
            Debug.Log(json.SaveToCompressedBase64());

            #endif
        }

        public static void Reset()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("RESET");
        }

        private JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Mute", Convert.ToString(Mute) }
            };
        }

        private static Profile FromJson(JSONNode json)
        {
            var profile = new Profile
            {
                Mute = bool.Parse(json["Settings"])
            };

            return profile;
        }
    }
}