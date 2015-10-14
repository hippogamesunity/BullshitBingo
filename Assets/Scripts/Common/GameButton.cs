using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public enum EventType
    {
        Down,
        Up
    }

    public class GameButton : Script
    {
        [Header("Event")]
        public EventType Type = EventType.Up;
        public MonoBehaviour Listener;
        public string Method;
        public string Params;
        [Header("Tween Params")]
        public float TweenTimeDown = 0.2f;
        public float TweenTimeUp = 0.2f;
        public float ScaleDown = 1.1f;
        public Color ColorDown = Color.white;
        public Color ColorUp = Color.white;
        public Color ColorDisabled = ColorHelper.GetColor(80, 80, 80);
        public Texture TextureDisabled;
        public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 1, 0));

        public event Action OnDown = () => {};
        public event Action OnUp = () => {};

        protected Texture Texture;
        [HideInInspector]
        public bool Pressed;

        public void Update()
        {
            if (Pressed)
            {
                if (!collider.bounds.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    Tween(false);
                    Pressed = false;
                }
            }
        }

        public bool Enabled
        {
            get { return collider.enabled; }
            set { Enable(value, value ? TweenTimeUp : TweenTimeDown); }
        }

        public void Enable(bool value, float duration)
        {
            if (Enabled != value)
            {
                collider.enabled = value;

                if (duration > 0)
                {
                    TweenColor.Begin(gameObject, duration, value ? ColorUp : ColorDisabled).animationCurve = AnimationCurve;
                    TweenScale.Begin(gameObject, duration, Vector3.one).animationCurve = AnimationCurve;
                }
                else
                {
                    GetComponent<UIWidget>().color = value ? ColorUp : ColorDisabled;
                    transform.localScale = Vector3.one;
                }

                if (TextureDisabled == null) return;

                if (Texture == null)
                {
                    Texture = GetComponent<UIWidget>().mainTexture;
                }

                GetComponent<UIWidget>().mainTexture = value ? Texture : TextureDisabled;
            }
        }

        public virtual void OnPress(bool down)
        {
            if (!enabled || QuickPressDeniedOnTween()) return;

            Tween(down);

            if (down)
            {
                OnDown();

                if (Type == EventType.Down)
                {
                    Action();
                }
            }
            else if (Pressed)
            {
                OnUp();

                if (Type == EventType.Up)
                {
                    Action();
                }
            }

            Pressed = down;
        }

        protected virtual void Action()
        {
            if (Listener == null || Method == null) return;

            if (Params == null)
            {
                Listener.SendMessage(Method);
            }
            else
            {
                Listener.SendMessage(Method, Params);
            }
        }

        protected virtual void Tween(bool down)
        {
            if (down)
            {
                TweenColor.Begin(gameObject, TweenTimeDown, ColorDown).animationCurve = AnimationCurve;
                TweenScale.Begin(gameObject, TweenTimeDown, ScaleDown * Vector3.one).animationCurve = AnimationCurve;

            }
            else
            {
                TweenColor.Begin(gameObject, TweenTimeUp, ColorUp).animationCurve = AnimationCurve;
                TweenScale.Begin(gameObject, TweenTimeUp, Vector3.one).animationCurve = AnimationCurve;
            }
        }

        protected bool QuickPressDeniedOnTween()
        {
            var parent = transform.parent;

            while (parent != null)
            {
                var tweener = parent.GetComponent<UITweener>();

                if (tweener != null && tweener.enabled)
                {
                    return true;
                }

                parent = parent.parent;
            }

            return false;
        }
    }
}