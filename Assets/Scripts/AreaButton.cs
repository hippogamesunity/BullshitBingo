using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts
{
    public class AreaButton : GameButton
    {
        public TextAsset Data;

        private List<string> _words; 

        public void Awake()
        {
            _words = Data.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Where(i => !string.IsNullOrEmpty(i)).Distinct().ToList();
        }

        protected override void Action()
        {
            UI.Navigator.StartGame(_words);
        }
    }
}