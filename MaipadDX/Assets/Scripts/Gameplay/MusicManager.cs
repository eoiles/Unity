using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Gameplay
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;
        public AudioSource source;
        public AudioClip breakEffect;
        public AudioClip hitEffect;

        public float musicOffset = 0f;

        public float scheduleDuration = 3;

        public bool isStarted = false;
        public float startTime;
        public float timeSinceStart;
        public float smoothTimeSinceStart;
        
        public void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public IEnumerator RefreshMusic(string musicPath)
        {
            isStarted = false;

            var req = UnityWebRequestMultimedia.GetAudioClip("file://" + musicPath, AudioType.MPEG);
            yield return req.SendWebRequest();
            source.clip = DownloadHandlerAudioClip.GetContent(req);
            source.PlayScheduled(AudioSettings.dspTime + scheduleDuration);
            startTime = (float) AudioSettings.dspTime + scheduleDuration;

            isStarted = true;
        }

        private void Update()
        {
            if (!isStarted) return;
            
            var newTime = (float) AudioSettings.dspTime - startTime - musicOffset;

            if (timeSinceStart == newTime)
            {
                smoothTimeSinceStart += Time.unscaledDeltaTime;
            }
            else
            {
                timeSinceStart = newTime;
                smoothTimeSinceStart = timeSinceStart;
            }
        }

        public void PlayFx(bool isBreak)
        {
            source.PlayOneShot(isBreak ? breakEffect : hitEffect, isBreak ? 0.75f : 1f);
        }
    }
}
