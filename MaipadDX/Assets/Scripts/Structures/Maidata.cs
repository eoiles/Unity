using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Structures
{
    [Serializable]
    public class Maidata
    {
        public string title;
        public string artist;
        public string wholebpm;

        public float first,
            first1,
            first2,
            first3,
            first4,
            first5,
            first6;

        public string amsgFirst;

        public float tapofs,
            holdofs,
            slideofs,
            breakofs;

        public Difficulty easy = new Difficulty();

        public Difficulty basic = new Difficulty();

        public Difficulty advanced = new Difficulty();

        public Difficulty expert = new Difficulty();

        public Difficulty master = new Difficulty();

        public Difficulty remaster = new Difficulty();

        public float amsgTime;
        public string amsgContent;

        [Serializable]
        public class Difficulty
        {
            public string level;
            public string description;
            
            public string rawChart;
            public PadderData chart;
        }
    }
}
