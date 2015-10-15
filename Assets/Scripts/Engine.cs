using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts
{
    public class Engine : SingleScript<Engine>
    {
        public UIWidget Container;

        public void Awake()
        {
            DetectLanguage();
        }

        public void Start()
        {
            UI.Navigator.OpenMenu();
            Container.alpha = 0;
            TweenAlpha.Begin(Container.gameObject, 1, 1);
        }

        private static void DetectLanguage()
        {
            var dictionaries = new List<string> { "Common" };
            var bytes = dictionaries.Select(i => Resources.Load<TextAsset>("Localization/" + i).bytes).ToArray();

            Localization.LoadCSV(ByteHelper.Join(bytes));

            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                case SystemLanguage.Ukrainian:
                case SystemLanguage.Belarusian:
                    Localization.language = "Russian";
                    break;
                default:
                    Localization.language = "English";
                    break;
            }
        }
    }
}