using UnityEngine;

namespace Assets.Scripts.Common
{
    public class RoundedProgress : Script
    {
        public UIWidget Begin;
        public UIWidget End;
        public UIWidget Progress;

        public void Start()
        {
            Reset();
        }

        public void Go(float duration)
        {
            Begin.enabled = End.enabled = Progress.enabled = true;
            End.transform.position = Begin.transform.position;
            Progress.transform.localScale = new Vector3(0, 1);

            TweenScale.Begin(Progress.gameObject, duration, new Vector3(1, 1));
            TweenPosition.Begin(End.gameObject, duration, new Vector3(-Begin.transform.localPosition.x, 0));
        }

        public void Reset()
        {
            Begin.enabled = End.enabled = Progress.enabled = false;
        }
    }
}