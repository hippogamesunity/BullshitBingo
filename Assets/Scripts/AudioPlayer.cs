using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : SingleScript<AudioPlayer>
    {
        public void PlayBullshit()
        {
            PlayEffect("Bullshit");
        }

        public void StopPlaying()
        {
            foreach (var audioSource in FindObjectsOfType<AudioSource>())
            {
                Destroy(audioSource);
            }
        }

        private bool _stoped;

        private void PlayEffect(string path)
        {
            var clip = (AudioClip) Resources.Load("Audio/" + path);
            var audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.volume = GetComponent<AudioSource>().volume;
            audioSource.PlayOneShot(clip);
            Destroy(audioSource, clip.length);
        }
    }
}