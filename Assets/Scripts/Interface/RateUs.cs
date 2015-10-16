using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class RateUs : BaseInterface<RateUs>
    {
        public override void Refresh()
        {
        }

        public void Rate()
        {
            Application.OpenURL(PlanformDependedSettings.StoreLink);
            Close();
        }
    }
}