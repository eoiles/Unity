using System;
using System.IO;
using System.Linq;
using Helpers;
using Structures;
using UnityEngine;

namespace Gameplay
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        public Maidata dataReference;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Application.persistentDataPath);
        
            var songs = Directory.GetDirectories(Application.persistentDataPath)
                .Select(dir => new DirectoryInfo(dir)).ToList();

            dataReference = MaidataInterpreter.LoadSong(songs[0], out var audioPath);

            StartCoroutine(MusicManager.Instance.RefreshMusic(audioPath));

            GameManager.Instance.AddNotes();
        }
    }
}
