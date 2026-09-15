using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieRace
{
    public class UIHealthBarView : MonoBehaviour
    {
        [SerializeField] private Image healthFill;
        [SerializeField] private Image damageFill;
        [SerializeField] private bool hideIfFullHp = false;
        [SerializeField] private float duration = .5f;

        private CanvasGroup group;

        protected void Awake()
        {
            this.group = this.GetOrAddComponent<CanvasGroup>();
        }

        public void SetValue(float current, float max)
        {
            float targetFill = max > 0f ? Mathf.Clamp01(current / max) : 0f;
            this.healthFill.fillAmount = targetFill;

            this.damageFill.DOKill();
            this.damageFill.DOFillAmount(targetFill, duration);

            if (hideIfFullHp)
                this.group.alpha = current == max ? 0f : 1f;
        }
    }
}