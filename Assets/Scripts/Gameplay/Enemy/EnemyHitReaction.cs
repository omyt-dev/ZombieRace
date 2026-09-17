using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ZombieRace
{
    public class EnemyHitReaction : MonoBehaviour
    {
        [SerializeField] private OverrideTransform overrideTransform;

        private float angle = 20f;
        private float duration = .15f;
        private float weight;
        private float targetWeight;

        public void Initialize(float angle, float duration)
        {
            this.angle = angle;
            this.duration = duration;
        }

        protected void Update()
        {
            if (Mathf.Approximately(this.weight, this.targetWeight))
                return;

            this.weight = Mathf.MoveTowards(this.weight, this.targetWeight, Time.deltaTime / this.duration);
            this.overrideTransform.weight = this.weight;

            if (Mathf.Approximately(this.weight, 0f))
                this.overrideTransform.data.rotation = Vector3.zero;
        }

        public void Play(Vector3 hitDirection)
        {
            if (hitDirection.FlatY().sqrMagnitude < 0.001f)
                return;

            Vector3 localDirection = this.transform.InverseTransformDirection(hitDirection.normalized);

            float pitch = localDirection.z * this.angle;
            float roll = -localDirection.x * this.angle;

            this.overrideTransform.data.rotation = new Vector3(pitch, 0f, roll);
            this.targetWeight = 1f;

            CancelInvoke(nameof(Reset));
            Invoke(nameof(Reset), this.duration);
        }

        private void Reset()
        {
            this.targetWeight = 0f;
        }
    }
}
