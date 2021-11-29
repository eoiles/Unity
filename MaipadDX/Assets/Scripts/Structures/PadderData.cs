using System;
using System.Collections.Generic;

namespace Structures
{
    [Serializable]
    public class PadderData
    {
        public Dictionary<float, float> tempoList = new Dictionary<float, float>();
        public Dictionary<float, float> signatureList = new Dictionary<float, float>();
        
        public List<Tap> notesBt1 = new List<Tap>();
        public List<Tap> notesBt2 = new List<Tap>();
        public List<Tap> notesBt3 = new List<Tap>();
        public List<Tap> notesBt4 = new List<Tap>();
        public List<Tap> notesBt5 = new List<Tap>();
        public List<Tap> notesBt6 = new List<Tap>();
        public List<Tap> notesBt7 = new List<Tap>();
        public List<Tap> notesBt8 = new List<Tap>();

        [Serializable]
        public class Tap : NoteObject
        {
            public Tap(float timing, Type type)
            {
                this.timing = timing;
                this.type = type;
            }

            public float timing;
        }
        
        [Serializable]
        public abstract class NoteObject
        {
            public Type type;
        }
        
        public enum Type
        {
            Normal,
            Each,
            Break
        }
    }
}
