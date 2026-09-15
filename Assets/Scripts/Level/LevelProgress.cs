using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class LevelProgress : ITickable, IResetable
    {
        private readonly CarController carController;

        public float Distance { get; private set; }
        public float LevelLength { get; }

        public float Progress => Mathf.Clamp01(this.LevelLength > 0f ? this.Distance / this.LevelLength : 0f);
        public bool IsCompleted => this.Distance >= this.LevelLength;

        public event Action<float> ProgressChanged;
        public event Action Completed;

        public LevelProgress(CarController carController)
        {
            this.carController = carController;
            this.LevelLength = 20f;
        }

        public void Tick()
        {
            float distance = this.carController.transform.position.z;

            if (distance == Distance)
                return;

            this.Distance = distance;
            this.ProgressChanged?.Invoke(this.Progress);

            StylizedLog.Log("LevelProgress", $"{Distance}/{LevelLength}", StylizedLog.Orange);

            if (this.IsCompleted)
                this.Completed?.Invoke();
        }

        public void Reset()
        {
            this.Distance = 0f;
            this.ProgressChanged?.Invoke(0f);
        }
    }
}
