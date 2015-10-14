using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Tweens;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class Game : BaseInterface<Game>
    {
        public Transform Workflow;
        public GameButton BingoButton;
        public BullshitButton[,] Buttons;

        public void Initialize(List<string> words)
        {
            words = words.Shuffle().Take(25).ToList();
            
            if (Buttons != null)
            {
                foreach (var button in Buttons)
                {
                    if (button == null) continue;

                    Destroy(button.gameObject);
                }
            }

            Buttons = new BullshitButton[5, 5];

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (x == 2 && y == 2) continue;

                    var button = PrefabsHelper.Instantiate("BullshitButton", Workflow).GetComponent<BullshitButton>();

                    button.transform.localPosition = new Vector3(-500 + 250 * x, 300 - 150 * y);
                    button.Initialize(x, y, words[x + y * 5]);

                    Buttons[x, y] = button;
                }
            }

            BingoButton.Enabled = false;
            BingoButton.transform.localScale = Vector3.one;
            BingoButton.GetComponent<ScaleSpring>().enabled = false;
        }

        public override void Refresh()
        {
        }

        public void CheckBingo()
        {
            List<BullshitButton> buttons;

            var bullshit = CheckBullshit(out buttons);

            BingoButton.Enabled = bullshit;
            BingoButton.GetComponent<ScaleSpring>().enabled = bullshit;

            foreach (var button in Buttons)
            {
                if (button == null || buttons.Contains(button)) continue;

                button.SetState(button.Bullshit ? 1 : 0);
            }

            if (bullshit)
            {
                foreach (var button in buttons)
                {
                    button.SetState(2);
                }
            }
        }

        public void PlayBullshit()
        {
            AudioPlayer.Instance.StopPlaying();
            AudioPlayer.Instance.PlayBullshit();
        }

        private bool CheckBullshit(out List<BullshitButton> buttons)
        {
            buttons = new List<BullshitButton>();

            for (var i = 0; i < 5; i++)
            {
                var bullshitX = true;
                var bullshitY = true;

                for (var j = 0; j < 5; j++)
                {
                    if (i == 2 && j == 2) continue;

                    if (!Buttons[i, j].Bullshit)
                    {
                        bullshitX = false;
                    }

                    if (!Buttons[j, i].Bullshit)
                    {
                        bullshitY = false;
                    }
                }

                if (bullshitX)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (i == 2 && j == 2) continue;

                        buttons.Add(Buttons[i, j]);
                    }

                    return true;
                }

                if (bullshitY)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (i == 2 && j == 2) continue;

                        buttons.Add(Buttons[j, i]);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}