using System;
using System.Collections.Generic;

namespace Assets.Scripts.Common
{
    public class SelectButton : GameButton
    {
        public event Action OnSelect = () => { };
        public event Action OnRelease = () => { };
        public event Action OnConfirm = () => { };

        public int Tag = 0;
        public bool Selected;

        private static readonly SortedDictionary<int, SelectButton> SelectedButtons = new SortedDictionary<int, SelectButton>();

        public void Start()
        {
            if (Selected)
            {
                OnPress();
            }
        }

        public void Select()
        {
            if (!Selected)
            {
                OnPress();
            }
        }

        public void Release()
        {
            if (!Selected) return;

            SelectedButtons.Remove(Tag);
            Tween(false);
            Selected = false;
        }

        public override void OnPress(bool down)
        {
            if (enabled && down && !QuickPressDeniedOnTween())
            {
                OnPress();
            }
        }

        public void OnPress()
        {
            Action();
            OnSelect();

            if (SelectedButtons.ContainsKey(Tag))
            {
                if (SelectedButtons[Tag] == this)
                {
                    OnConfirm();
                }
                else if (SelectedButtons[Tag] != null)
                {
                    SelectedButtons[Tag].Tween(false);
                    SelectedButtons[Tag].OnRelease();
                    SelectedButtons[Tag].Selected = false;
                }
            }

            Tween(true);
            SelectedButtons[Tag] = this;
            Selected = true;
        }
    }
}