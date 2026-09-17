using System;
using System.Collections;
using UnityEngine;

namespace ZombieRace
{
    public class HitFlash : MonoBehaviour
    {
        private static readonly int EmissionColor =
            Shader.PropertyToID("_EmissionColor");

        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Color flashColor = Color.white;
        [SerializeField] private float intensity = 5f;
        [SerializeField] private float duration = 0.08f;

        private MaterialPropertyBlock propertyBlock;
        private Coroutine flashCoroutine;

        private void Awake()
        {
            this.propertyBlock = new MaterialPropertyBlock();
        }

        public void Play()
        {
            if (this.flashCoroutine != null)
                this.StopCoroutine(this.flashCoroutine);

            this.flashCoroutine = this.StartCoroutine(this.Flash());
        }

        public void ResetFlash()
        {
            if (this.flashCoroutine != null)
            {
                this.StopCoroutine(this.flashCoroutine);
                this.flashCoroutine = null;
            }

            this.SetEmission(Color.black);
        }

        private IEnumerator Flash()
        {
            this.SetEmission(
                this.flashColor * this.intensity);

            yield return new WaitForSeconds(this.duration);

            this.SetEmission(Color.black);
            this.flashCoroutine = null;
        }

        private void SetEmission(Color color)
        {
            for (int i = 0; i < this.renderers.Length; i++)
            {
                this.propertyBlock.Clear();

                this.propertyBlock.SetColor(
                    EmissionColor,
                    color);

                this.renderers[i].SetPropertyBlock(
                    this.propertyBlock);
            }
        }
    }
}
