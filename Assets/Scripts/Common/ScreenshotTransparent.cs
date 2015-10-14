﻿using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class ScreenshotTransparent : Script
    {
        public int Width = 1920;
        public int Height = 1280;

        public static string ScreenShotName(int width, int height)
        {
            return string.Format("{0}/Screenshot_{1}x{2}_{3}.png", Application.dataPath, width, height, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                var renderTexture = new RenderTexture(Width, Height, 24);
                var texture2D = new Texture2D(Width, Height, TextureFormat.ARGB32, false);

                camera.targetTexture = renderTexture;
                camera.Render();
                RenderTexture.active = renderTexture;
                texture2D.ReadPixels(new Rect(0, 0, Width, Height), 0, 0);
                camera.targetTexture = null;
                RenderTexture.active = null;
                Destroy(renderTexture);
                
                var bytes = texture2D.EncodeToPNG();
                var filename = ScreenShotName(Width, Height);

                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log(string.Format("Took screenshot to: {0}", filename));
            }
        }
    }
}