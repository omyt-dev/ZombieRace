using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieRace
{
    public class UIProgressBarView : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private RectTransform progressArea;
        [SerializeField] private RectTransform currentMarker;
        [SerializeField] private TMP_Text currentText;
        [SerializeField] private TMP_Text maxText;
        [SerializeField] private string format = "{0}";

        public void SetValue(float currentValue, float maxValue)
        {
            float progress = maxValue > 0f ? Mathf.Clamp01(currentValue / maxValue) : 0f;

            this.fill.fillAmount = progress;

            Vector2 anchoredPosition = this.currentMarker.anchoredPosition;
            anchoredPosition.y = this.progressArea.rect.height * progress;
            this.currentMarker.anchoredPosition = anchoredPosition;

            this.currentText.text = string.Format(format, Mathf.FloorToInt(currentValue));
            this.maxText.text = string.Format(format, Mathf.FloorToInt(maxValue));
        }
    }
}
