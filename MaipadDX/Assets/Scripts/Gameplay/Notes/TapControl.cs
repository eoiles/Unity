using System;
using DG.Tweening;
using Dreamteck.Splines;
using Structures;
using UnityEngine;

namespace Gameplay.Notes
{
    public class TapControl : MonoBehaviour
    {
        public SpriteRenderer noteRenderer, guideRenderer;
        public Sprite normalSprite, eachSprite, breakSprite;
        public Transform guideStrip;

        public PadderData.Tap reference;

        public SplineUser parentSpline;

        public bool isIntro = true;

        public Color tapColor, eachColor, breakColor, starColor;

        public float PlayPercentage => 1 - DeltaDspTime /
                                       GameManager.Instance.fallDuration;
        public float DeltaDspTime => reference.timing - MusicManager.Instance.smoothTimeSinceStart;
        
        // Update is called once per frame
        void Update()
        {
            if (!MusicManager.Instance.isStarted || DeltaDspTime >= GameManager.Instance.fadeInDuration + GameManager.Instance.fallDuration) return;

            if (isIntro)
            {
                isIntro = false;
                transform.DOScale(new Vector3(1, 1, 1),
                    GameManager.Instance.fadeInDuration);
            }

            var clampPercentage = Mathf.Clamp(PlayPercentage * 1.15f, 0.15f, 1f);
            
            transform.localPosition = parentSpline.Evaluate(PlayPercentage).position;
            guideStrip.localScale = new Vector3(clampPercentage, clampPercentage, 0);

            if (PlayPercentage >= 1)
            {
                StatisticsManager.Instance.combo++;
                
                UIManager.Instance.ChangeText($"<size=32>combo</size>\n{StatisticsManager.Instance.combo}");
                UIManager.Instance.Pulse();
                gameObject.SetActive(false);
            }
        }

        public void Init()
        {
            noteRenderer.sprite = reference.type switch
            {
                PadderData.Type.Normal => normalSprite,
                PadderData.Type.Each => eachSprite,
                PadderData.Type.Break => breakSprite,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            guideRenderer.color = reference.type switch
            {
                PadderData.Type.Normal => tapColor,
                PadderData.Type.Each => eachColor,
                PadderData.Type.Break => breakColor,
                _ => throw new ArgumentOutOfRangeException()
            };

            transform.parent = parentSpline.transform;
            var destination = parentSpline.Evaluate(1).position;
            Vector3 v3Dir = transform.position - destination;
            float angle = Mathf.Atan2(v3Dir.y, v3Dir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle + 90);
        }
    }
}
