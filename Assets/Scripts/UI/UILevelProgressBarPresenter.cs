using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class UILevelProgressBarPresenter : BaseBehaviour
    {
        private LevelProgress levelProgress;
        private UIProgressBarView view;

        [Inject]
        private void Construct(LevelProgress levelProgress)
        {
            this.levelProgress = levelProgress;
            this.view = this.GetComponent<UIProgressBarView>();

            this.SubscribeEvent(
              () => this.levelProgress.ProgressChanged += this.OnProgressChanged,
              () => this.levelProgress.ProgressChanged -= this.OnProgressChanged);

            this.view.SetValue(this.levelProgress.Distance, this.levelProgress.LevelLength);
        }

        private void OnProgressChanged(float progress)
        {
            this.view.SetValue(this.levelProgress.Distance, this.levelProgress.LevelLength);
        }
    }
}
