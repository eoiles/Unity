using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Notes;
using Structures;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public float fallDuration = 0.4f;
        public float fadeInDuration = 0.1f;

        public void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public List<PadderData.Tap> autoSounds = new List<PadderData.Tap>();

        // Update is called once per frame
        void Update()
        {
            if (!MusicManager.Instance.isStarted) return;

            foreach (var v in autoSounds.Where(v => MusicManager.Instance.timeSinceStart > v.timing).ToList())
            {
                MusicManager.Instance.PlayFx(v.type == PadderData.Type.Break);
                autoSounds.Remove(v);
            }
        }

        public void AddNotes()
        {
            if (autoSounds.Count >= 1) return;

            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt1);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt2);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt3);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt4);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt5);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt6);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt7);
            autoSounds.AddRange(DataManager.Instance.dataReference.master.chart.notesBt8);

            RenderManager.Instance.RenderNotes(DataManager.Instance.dataReference.master.chart);
        }
    }
}