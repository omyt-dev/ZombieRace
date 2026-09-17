using DG.Tweening;
using UnityEngine;

namespace ZombieRace
{
    public class CarHitReaction : MonoBehaviour
    {
        [SerializeField] private Transform visualRoot;
        [SerializeField] private float squash = 0.9f;
        [SerializeField] private float stretch = 1.05f;
        [SerializeField] private float duration = 0.08f;

        private Vector3 startScale;

        private void Awake()
        {
            this.startScale = this.visualRoot.localScale;
        }

        public void Play()
        {
            this.visualRoot.DOKill();
            this.visualRoot.localScale = this.startScale;

            Vector3 squashScale = new Vector3(
                this.startScale.x * this.stretch,
                this.startScale.y * this.squash,
                this.startScale.z * this.stretch);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(this.visualRoot.DOScale(squashScale, this.duration).SetEase(Ease.OutQuad));
            sequence.Append(this.visualRoot.DOScale(this.startScale, this.duration).SetEase(Ease.OutBack));
        }

        public void ResetReaction()
        {
            this.visualRoot.DOKill();
            this.visualRoot.localScale = this.startScale;
        }
    }
}