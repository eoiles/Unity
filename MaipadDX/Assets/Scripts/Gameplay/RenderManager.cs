using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using Gameplay.Notes;
using Structures;
using UnityEngine;

namespace Gameplay
{
    public class RenderManager : MonoBehaviour
    {
        public static RenderManager Instance;

        public void Awake()
        {
            if (Instance == null) Instance = this;
        }
        
        public TapControl tapPrefab;

        public SplineUser splineBt1,
            splineBt2,
            splineBt3,
            splineBt4,
            splineBt5,
            splineBt6,
            splineBt7,
            splineBt8;

        public void RenderNotes(PadderData data)
        {
            RenderTaps(data.notesBt1, splineBt1);
            RenderTaps(data.notesBt2, splineBt2);
            RenderTaps(data.notesBt3, splineBt3);
            RenderTaps(data.notesBt4, splineBt4);
            RenderTaps(data.notesBt5, splineBt5);
            RenderTaps(data.notesBt6, splineBt6);
            RenderTaps(data.notesBt7, splineBt7);
            RenderTaps(data.notesBt8, splineBt8);
        }

        private List<TapControl> RenderTaps(List<PadderData.Tap> taps, SplineUser parent)
        {
            var ret = new List<TapControl>();
            
            foreach (var t in taps)
            {
                var tap = Instantiate(tapPrefab);
                tap.reference = t;
                tap.parentSpline = parent;
                tap.Init();

                ret.Add(tap);
            }

            return ret;
        }
    }
}
