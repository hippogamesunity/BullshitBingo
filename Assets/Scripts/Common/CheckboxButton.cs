using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class CheckboxButton : GameButton
    {
        public Texture Checked;
        public Texture Unckecked;
        public Func<bool> Condition;

        public override void OnPress(bool down)
        {
            base.OnPress(down);
            Refresh();
        }

        public void Refresh()
        {
            if (Condition != null)
            {
                Get<UITexture>().mainTexture = Condition() ? Checked : Unckecked;
            }
        }
    }
}