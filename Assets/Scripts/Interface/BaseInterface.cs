using Assets.Scripts.Common;

namespace Assets.Scripts.Interface
{
    public abstract class BaseInterface<T> : SingleScript<T> where T : Script
    {
        public TweenPanel Panel;

        public virtual void Open()
        {
            if (Opened) return;

            Panel.Hide(0f);
            Panel.Show();
            Refresh();
        }

        public virtual void Close()
        {
            Panel.Hide();
        }

        public virtual bool Opened
        {
            get { return Panel.Opened; }
        }

        public abstract void Refresh();
    }
}