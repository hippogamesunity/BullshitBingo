using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class Menu : BaseInterface<Menu>
    {
        public GameObject Ru;
        public GameObject En;
        public GameObject HelpButton;
        public GameObject ContinueButton;

        public void Start()
        {
            Refresh();
        }

        public void Initialize(bool restore)
        {
            HelpButton.SetActive(!restore);
            ContinueButton.SetActive(restore);
        }

        public override void Refresh()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                case SystemLanguage.Ukrainian:
                case SystemLanguage.Belarusian:
                    Ru.SetActive(true);
                    En.SetActive(false);
                    break;
                default:
                    Ru.SetActive(false);
                    En.SetActive(true);
                    break;
            }
        }
    }
}