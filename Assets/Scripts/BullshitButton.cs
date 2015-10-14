using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts
{
    public class BullshitButton : GameButton
    {
        public UILabel Text;
        public UITexture Background;

        [HideInInspector] public int X;
        [HideInInspector] public int Y;
        [HideInInspector] public bool Bullshit;

        public void Initialize(int x, int y, string word)
        {
            X = x;
            Y = y;
            Text.text = word;
        }

        protected override void Action()
        {
            Bullshit = !Bullshit;
            SetState(Bullshit ? 1 : 0);
            UI.Game.CheckBingo();
        }

        public void SetState(int state)
        {
            switch (state)
            {
                case 0:
                    Background.mainTexture = Resources.Load<Texture>("UI/ButtonGrey");
                    break;
                case 1:
                    Background.mainTexture = Resources.Load<Texture>("UI/ButtonYellow");
                    break;
                case 2:
                    Background.mainTexture = Resources.Load<Texture>("UI/ButtonRed");
                    break;
            }
        }
    }
}