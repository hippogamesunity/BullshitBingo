using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class Navigator : SingleScript<Navigator>
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (UI.Game.Opened || UI.Help.Opened)
                {
                    OpenMenu();
                }
                else
                {
                    Application.Quit();
                }
            }
        }

        public void StartGame(List<string> words)
        {
            UI.Menu.Close();
            UI.Game.Initialize(words);
            UI.Game.Open();
        }

        public void ContinueGame()
        {
            UI.Menu.Close();
            UI.Game.Open();
        }

        public void OpenMenu()
        {
            if (UI.Game.Opened)
            {
                UI.Game.Close();
                UI.Menu.Initialize(true);
            }
            else if (UI.Help.Opened)
            {
                UI.Help.Close();
                UI.Menu.Initialize(false);
            }
            else
            {
                UI.Menu.Initialize(false);
            }

            UI.Menu.Open();
        }

        public void OpenHelp()
        {
            UI.Menu.Close();
            UI.Help.Open();
        }

        public void CloseHelp()
        {
            UI.Help.Close();
            UI.Menu.Open();
        }
    }
}