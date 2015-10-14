using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class TweenPanel : Script
    {
        public TweenDirection TweenDirection;
        public bool UseCustomTweenPosition;
        public Vector3 CustomTweenPosition;
        public float DefaultTimeout = 0.4f;
        public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
        public bool Transparency;
     
        public bool Opened
        {
            get { return gameObject.activeSelf && transform.localPosition == Vector3.zero; }
        }

        public void Show()
        {
            Tween(true, DefaultTimeout, TweenDirection);
        }

        public void Show(float timeout)
        {
            Tween(true, timeout, TweenDirection);
        }

        public void Show(TweenDirection direction)
        {
            Tween(true, DefaultTimeout, direction);
        }

        public void Hide()
        {
            Tween(false, DefaultTimeout, TweenDirection);
        }

        public void Hide(float timeout)
        {
            Tween(false, timeout, TweenDirection);
        }

        public void Hide(TweenDirection direction)
        {
            Tween(false, DefaultTimeout, direction);
        }
    
        public void Hide(TweenDirection direction, float timeout)
        {
            Tween(false, timeout, direction);
        }

        private void Tween(bool show, float timeout, TweenDirection tweenDirection)
        {
            TaskScheduler.Kill(Id);

            Vector3 vector;

            if (show)
            {
                if (Opened)
                {
                    return;
                }

                Hide(tweenDirection, 0f);
            }

            if (show)
            {
                vector = Vector3.zero;
                gameObject.SetActive(true);
            }
            else
            {
                if (UseCustomTweenPosition)
                {
                    vector = new Vector2(CustomTweenPosition.x * transform.localScale.x, CustomTweenPosition.y * transform.localScale.y);
                }
                else
                {
                    vector = GetVector(tweenDirection, 1000 * Camera.main.aspect);
                }

                if (timeout > 0)
                {
                    TaskScheduler.CreateTask(() => gameObject.SetActive(false), Id, timeout);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }

            if (timeout > 0)
            {
                TweenPosition.Begin(gameObject, timeout, vector).animationCurve = AnimationCurve;

                if (Transparency)
                {
                    TaskScheduler.CreateTask(() => TweenAlpha.Begin(gameObject, timeout / 2, show ? 1 : 0), Id, show ? timeout / 2 : 0);
                }
            }
            else
            {
                transform.localPosition = vector;

                if (Transparency)
                {
                    GetComponent<UIPanel>().alpha = show ? 1 : 0;
                }
            }
        }

        private static Vector3 GetVector(TweenDirection tweenDirection, float aspect)
        {
            switch (tweenDirection)
            {
                case TweenDirection.Left:
                    return -Vector2.right * aspect;
                case TweenDirection.Right:
                    return Vector2.right * aspect;
                case TweenDirection.Up:
                    return Vector2.up*1000;
                case TweenDirection.Down:
                    return -Vector2.up*1000;
                default:
                    throw new Exception();
            }
        }
    }

    public enum TweenDirection { Left, Right, Up, Down }
}