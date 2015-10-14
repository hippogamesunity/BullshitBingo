namespace Assets.Scripts.Common
{
    public abstract class SingleScript<T> : Script where T : Script
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<T>()); }
        }
    }
}