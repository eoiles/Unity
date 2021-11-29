using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public TextMeshProUGUI displayUgui;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void ChangeText(string overrideText)
        {
            displayUgui.text = overrideText;
        }

        public void Pulse()
        {
            displayUgui.transform.DOComplete();
            displayUgui.transform.DOPunchScale(new Vector3(-0.1f, -0.1f), 0.15f);
        }
    }
}
